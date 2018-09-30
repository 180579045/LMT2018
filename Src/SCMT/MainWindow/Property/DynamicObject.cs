using NetPlan;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SCMTMainWindow.Property
{
    public class DynamicObject
    {        
        public static Type BuildTypeWithCustomAttributesOnMethod(string TypeName, List<MibLeafNodeInfo> mibList)
        {
            try
            {
                AppDomain currentDomain = Thread.GetDomain();
                AssemblyName myAsmName = new AssemblyName();

                myAsmName.Name = "MyAssembly";

                AssemblyBuilder myAsmBuilder = currentDomain.DefineDynamicAssembly(myAsmName, AssemblyBuilderAccess.RunAndSave);

                ModuleBuilder myModeBuilder = myAsmBuilder.DefineDynamicModule(myAsmName.Name, myAsmName.Name + ".dll");

                TypeBuilder myTypeBuilder = myModeBuilder.DefineType("TypeName", TypeAttributes.Public);
                //string类型
                Type[] ctorParams = new Type[] { typeof(string) };
                //bool类型
                Type[] ctorboolParams = new Type[] { typeof(bool) };
                ConstructorInfo classCtorInfo = typeof(DescriptionAttribute).GetConstructor(ctorParams);
                // CustomAttributeBuilder mCABuilder = new CustomAttributeBuilder(classCtorInfo, new object[] { "Joe Programmer" });
                // myTypeBuilder.SetCustomAttribute(mCABuilder);
                //存放各字段的默认值 在实例化对象的时候传入
                
                object[] b1 = new object[mibList.Count];
                Type[] constuctParmTypes = new Type[mibList.Count];
                for (int j = 0; j < mibList.Count; j++)
                {
                    Type syntax = GetTypeByName(mibList[j].mibAttri.mibSyntax);
                    constuctParmTypes[j] = syntax;
                }
                ConstructorBuilder constructorBuilder = myTypeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, constuctParmTypes);
                ILGenerator ctorIL = constructorBuilder.GetILGenerator();
                for (int i=0;i<mibList.Count;i++) {
                    FieldBuilder myField = null;
                    Type syntax= GetTypeByName(mibList[i].mibAttri.mibSyntax);
                    constuctParmTypes[i] = syntax;
                    
                    object o1=mibList[0].m_strRealValue;
                    //取mib属性名首字母小写作为私有属性
                    string fieldNameprivate = TpLowerFirst(mibList[i].mibAttri.childNameMib);
                    myField = myTypeBuilder.DefineField(fieldNameprivate, syntax, FieldAttributes.Private);
                    string propertyPublic = ToUpperFirst(fieldNameprivate);
                    //公有属性
                    PropertyBuilder propertyBuilder = myTypeBuilder.DefineProperty(propertyPublic, PropertyAttributes.HasDefault, syntax, null);
                    //添加描述Attribute
                    classCtorInfo = typeof(DescriptionAttribute).GetConstructor(ctorParams);                    
                    CustomAttributeBuilder descriptionAttribute = new CustomAttributeBuilder(classCtorInfo, new object[] { mibList[i].mibAttri.detailDesc});
                    propertyBuilder.SetCustomAttribute(descriptionAttribute);
                    //添加显示名称Attribute
                    classCtorInfo = typeof(DisplayNameAttribute).GetConstructor(ctorParams);
                    CustomAttributeBuilder displayNameAttribute = new CustomAttributeBuilder(classCtorInfo, new object[] { mibList[i].mibAttri.childNameCh });
                    propertyBuilder.SetCustomAttribute(displayNameAttribute);
                    //添加是否只读Attribute
                    classCtorInfo = typeof(ReadOnlyAttribute).GetConstructor(ctorboolParams);
                    CustomAttributeBuilder readOnlyAttribute = new CustomAttributeBuilder(classCtorInfo, new object[] { mibList[i].m_bReadOnly });
                    propertyBuilder.SetCustomAttribute(readOnlyAttribute);
                    //添加是否显示Attribute
                    classCtorInfo = typeof(BrowsableAttribute).GetConstructor(ctorboolParams);
                    CustomAttributeBuilder browsableAttribute = new CustomAttributeBuilder(classCtorInfo, new object[] { mibList[i].m_bVisible });
                    propertyBuilder.SetCustomAttribute(browsableAttribute);
                    //构造get方法
                    MethodBuilder getPropertyBuilder = myTypeBuilder.DefineMethod("get", MethodAttributes.Public, syntax, Type.EmptyTypes);
                    ILGenerator getALL = getPropertyBuilder.GetILGenerator();
                    getALL.Emit(OpCodes.Ldarg_0);
                    getALL.Emit(OpCodes.Ldfld, myField);
                    getALL.Emit(OpCodes.Ret);
                    //构造set方法
                    MethodBuilder setPropertyBuilder = myTypeBuilder.DefineMethod("set", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new Type[] { syntax });
                    ILGenerator setAll = setPropertyBuilder.GetILGenerator();
                    setAll.Emit(OpCodes.Ldarg_0);
                    setAll.Emit(OpCodes.Ldarg_1);
                    setAll.Emit(OpCodes.Stfld, myField);
                    setAll.Emit(OpCodes.Ret);
                    propertyBuilder.SetGetMethod(getPropertyBuilder);
                    propertyBuilder.SetSetMethod(getPropertyBuilder);

                    //
                    ctorIL.Emit(OpCodes.Ldarg_0);
                    ctorIL.Emit(OpCodes.Ldarg_S,i);
                    ctorIL.Emit(OpCodes.Stfld, myField);
                }
                ctorIL.Emit(OpCodes.Ret);
                return myTypeBuilder.CreateType();  
                
            }
            catch (Exception e)
            {
                
                MessageBox.Show(e.Message);

            }

            return null;
        }
        public static Type GetTypeByName(string name) {
            switch(name){
                case "string":
                    return typeof(string);
                case "Integer":
                    return typeof(int);
                case "enum":
                    return typeof(Enum);
                default:
                    return null;
            }
        }
        public static string ToUpperFirst(string originString)
        {
            return originString.First().ToString().ToUpper() + originString.Substring(1);
        }
        /// <summary>
        /// 将字符串的首字母小写
        /// </summary>
        /// <param name="originString"></param>
        /// <returns></returns>
        public static string TpLowerFirst(string originString)
        {

            return originString.First().ToString().ToLower() + originString.Substring(1);

        }


    }
}
