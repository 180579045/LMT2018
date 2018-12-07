using CommonUtility;
using LogManager;
using MIBDataParser.JSONDataMgr;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetPlan;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MAP_DEVTYPE_DEVATTRI = System.Collections.Generic.Dictionary<NetPlan.EnumDevType, System.Collections.Generic.List<NetPlan.DevAttributeInfo>>;


namespace NetPlan.Tests
{
    [TestClass()]
    public class NPECheckRulesHelperTests
    {
        public NPECheckRulesHelperTests()
        {
            Log.SetLogFileName("NPECheckRulesHelperTests.log");
        }
        private async Task simConnectEnb()
        {
            CSEnbHelper.SetCurEnbAddr("172.27.245.92");
            var db = Database.GetInstance();
            var result = await db.initDatabase("172.27.245.92");
        }
        private DevAttributeInfo SimAddRru(string index, int typeIndex, int slot, int OfpWorkMode)
        {
            DevAttributeInfo oneRru = new DevAttributeInfo(EnumDevType.rru, index);
            oneRru.m_mapAttributes["netRRURowStatus"].m_strLatestValue = 4.ToString();
            oneRru.m_mapAttributes["netRRUManufacturerIndex"].m_strLatestValue = 4.ToString();
            oneRru.m_mapAttributes["netRRUTypeIndex"].m_strLatestValue = typeIndex.ToString();
            oneRru.m_mapAttributes["netRRUOfpWorkMode"].m_strLatestValue = OfpWorkMode.ToString(); //负荷
            oneRru.m_mapAttributes["netRRUAccessRackNo"].m_strLatestValue = 0.ToString();
            oneRru.m_mapAttributes["netRRUAccessShelfNo"].m_strLatestValue = 0.ToString();
            oneRru.m_mapAttributes["netRRUAccessSlotNo"].m_strLatestValue = slot.ToString();
            oneRru.m_mapAttributes["netRRUAccessBoardType"].m_strLatestValue = 241.ToString();
            oneRru.m_mapAttributes["netRRUOfp1AccessOfpPortNo"].m_strLatestValue = 0.ToString();
            oneRru.m_mapAttributes["netRRUOfp1AccessLinePosition"].m_strLatestValue = 1.ToString();
            oneRru.m_mapAttributes["netRRUOfp2AccessOfpPortNo"].m_strLatestValue = 3.ToString();
            oneRru.m_mapAttributes["netRRUOfp2AccessLinePosition"].m_strLatestValue = 1.ToString();
            oneRru.m_mapAttributes["netRRUHubNo"].m_strLatestValue = "-1";
            oneRru.m_mapAttributes["netRRUOfp1AccessEthernetPort"].m_strLatestValue = "-1";
            oneRru.m_mapAttributes["netRRUOfp2AccessEthernetPort"].m_strLatestValue = "-1";
            oneRru.m_mapAttributes["netRRUGsmSwitch"].m_strLatestValue = "0";

            oneRru.m_mapAttributes["netRRURowStatus"].m_strOriginValue = 6.ToString();
            oneRru.m_mapAttributes["netRRUManufacturerIndex"].m_strOriginValue = 4.ToString();
            oneRru.m_mapAttributes["netRRUTypeIndex"].m_strOriginValue = typeIndex.ToString();
            oneRru.m_mapAttributes["netRRUOfpWorkMode"].m_strOriginValue = OfpWorkMode.ToString();
            oneRru.m_mapAttributes["netRRUAccessRackNo"].m_strOriginValue = 0.ToString();
            oneRru.m_mapAttributes["netRRUAccessShelfNo"].m_strOriginValue = 0.ToString();
            oneRru.m_mapAttributes["netRRUAccessSlotNo"].m_strOriginValue = "-1";
            oneRru.m_mapAttributes["netRRUAccessBoardType"].m_strOriginValue = 241.ToString();
            oneRru.m_mapAttributes["netRRUOfp1AccessOfpPortNo"].m_strOriginValue = 0.ToString();
            oneRru.m_mapAttributes["netRRUOfp1AccessLinePosition"].m_strOriginValue = 1.ToString();
            oneRru.m_mapAttributes["netRRUOfp2AccessOfpPortNo"].m_strOriginValue = 3.ToString();
            oneRru.m_mapAttributes["netRRUOfp2AccessLinePosition"].m_strOriginValue = 1.ToString();
            oneRru.m_mapAttributes["netRRUHubNo"].m_strOriginValue = "-1";
            oneRru.m_mapAttributes["netRRUOfp1AccessEthernetPort"].m_strOriginValue = "-1";
            oneRru.m_mapAttributes["netRRUOfp2AccessEthernetPort"].m_strOriginValue = "-1";
            oneRru.m_mapAttributes["netRRUGsmSwitch"].m_strOriginValue = "0";
            return oneRru;
        }

        private DevAttributeInfo SimAddBoard(string index, string typeIndex, string row)
        {
            DevAttributeInfo oneBoard = new DevAttributeInfo(EnumDevType.board, index);
            oneBoard.m_mapAttributes["netBoardRowStatus"].m_strLatestValue = row;
            oneBoard.m_mapAttributes["netBoardType"].m_strLatestValue = typeIndex.ToString();
            oneBoard.m_mapAttributes["netBoardWorkMode"].m_strLatestValue = 2.ToString();
            oneBoard.m_mapAttributes["netBoardIrFrameType"].m_strLatestValue = 1.ToString();

            oneBoard.m_mapAttributes["netBoardRowStatus"].m_strOriginValue = 6.ToString();
            oneBoard.m_mapAttributes["netBoardType"].m_strOriginValue = typeIndex.ToString();
            oneBoard.m_mapAttributes["netBoardWorkMode"].m_strOriginValue = 2.ToString();
            oneBoard.m_mapAttributes["netBoardIrFrameType"].m_strOriginValue = 1.ToString();
            return oneBoard;
        }

        private DevAttributeInfo SimAddAnt(string index, string typeIndex, string rowStatus)
        {
            DevAttributeInfo oneAnt = new DevAttributeInfo(EnumDevType.ant, index);
            oneAnt.m_mapAttributes["netAntArrayRowStatus"].m_strLatestValue = rowStatus.ToString();
            oneAnt.m_mapAttributes["netAntArrayVendorIndex"].m_strLatestValue = 17.ToString();
            oneAnt.m_mapAttributes["netAntArrayTypeIndex"].m_strLatestValue = typeIndex.ToString();
            oneAnt.m_mapAttributes["netAntArrayHalfPowerBeamWidth"].m_strLatestValue = 65.ToString();

            oneAnt.m_mapAttributes["netAntArrayNo"].m_strOriginValue = index;
            oneAnt.m_mapAttributes["netAntArrayRowStatus"].m_strOriginValue = 6.ToString();
            oneAnt.m_mapAttributes["netAntArrayVendorIndex"].m_strOriginValue = 17.ToString();
            oneAnt.m_mapAttributes["netAntArrayTypeIndex"].m_strOriginValue = typeIndex.ToString();
            oneAnt.m_mapAttributes["netAntArrayHalfPowerBeamWidth"].m_strOriginValue = 65.ToString();
            return oneAnt;
        }

        private DevAttributeInfo SimAddRruAnt(string index, int antNo, int antPort, string id, string txrx)
        {
            DevAttributeInfo oneRruAnt = new DevAttributeInfo(EnumDevType.rru_ant, index);
            oneRruAnt.m_mapAttributes["netSetRRUPortWithAntennaRowStatus"].m_strLatestValue = 4.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortAntArrayNo"].m_strLatestValue = antNo.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortAntArrayPathNo"].m_strLatestValue = antPort.ToString();
            string[] lcId = id.Split('.');
            oneRruAnt.m_mapAttributes["netSetRRUPortSubtoLocalCellId"].m_strLatestValue = lcId[0].ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortSubtoLocalCellId2"].m_strLatestValue = lcId[1].ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortSubtoLocalCellId3"].m_strLatestValue = lcId[2].ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortSubtoLocalCellId4"].m_strLatestValue = lcId[3].ToString();

            oneRruAnt.m_mapAttributes["netSetRRUPortTxRxStatus"].m_strLatestValue = txrx.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortWithAntennaRowStatus"].m_strOriginValue = 6.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortAntArrayNo"].m_strOriginValue = antNo.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortAntArrayPathNo"].m_strOriginValue = antPort.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortSubtoLocalCellId"].m_strOriginValue = "-1";
            oneRruAnt.m_mapAttributes["netSetRRUPortSubtoLocalCellId2"].m_strOriginValue = "-1";
            oneRruAnt.m_mapAttributes["netSetRRUPortSubtoLocalCellId3"].m_strOriginValue = "-1";
            oneRruAnt.m_mapAttributes["netSetRRUPortSubtoLocalCellId4"].m_strOriginValue = "-1";
            oneRruAnt.m_mapAttributes["netSetRRUPortTxRxStatus"].m_strOriginValue = txrx.ToString();
            return oneRruAnt;
        }

        private DevAttributeInfo SimAddLcCtrl(string index)
        {
            DevAttributeInfo oneNetLcCtr = new DevAttributeInfo(EnumDevType.netLcCtr, index);
            oneNetLcCtr.m_mapAttributes["netPlanControlLcConfigSwitch"].m_strLatestValue = 0.ToString();
            return oneNetLcCtr;
        }

        private DevAttributeInfo SimAddNetLc(string index, string rowStatus, string band, string width)
        {
            DevAttributeInfo oneNetLc = new DevAttributeInfo(EnumDevType.netLc, index);
            oneNetLc.m_mapAttributes["netLcRowStatus"].m_strLatestValue = rowStatus.ToString();
            oneNetLc.m_mapAttributes["netLcFreqBand"].m_strLatestValue = band.ToString();
            oneNetLc.m_mapAttributes["netLcFreqBandWidth"].m_strLatestValue = width.ToString();
            oneNetLc.m_mapAttributes["netLcAntCombinationFlag"].m_strLatestValue = 0.ToString();
            oneNetLc.m_mapAttributes["netLcAppScene"].m_strLatestValue = 1.ToString();
            oneNetLc.m_mapAttributes["netLcAntArrayMode"].m_strLatestValue = 1.ToString();
            oneNetLc.m_mapAttributes["netLcIrCompressMode"].m_strLatestValue = 1.ToString();
            oneNetLc.m_mapAttributes["netLcAntPortNum"].m_strLatestValue = 1.ToString();
            oneNetLc.m_mapAttributes["netLcFrameType"].m_strLatestValue = 1.ToString();
            oneNetLc.m_mapAttributes["netLcCellCombineEnhancedSwitch"].m_strLatestValue = 0.ToString();
            oneNetLc.m_mapAttributes["netLcSdcFuncSwitch"].m_strLatestValue = 1.ToString();

            oneNetLc.m_mapAttributes["netLcRowStatus"].m_strOriginValue = 6.ToString();
            oneNetLc.m_mapAttributes["netLcFreqBand"].m_strOriginValue = band.ToString();
            oneNetLc.m_mapAttributes["netLcFreqBandWidth"].m_strOriginValue = width.ToString();
            oneNetLc.m_mapAttributes["netLcAntCombinationFlag"].m_strOriginValue = 0.ToString();
            oneNetLc.m_mapAttributes["netLcAppScene"].m_strOriginValue = 1.ToString();
            oneNetLc.m_mapAttributes["netLcAntArrayMode"].m_strOriginValue = 1.ToString();
            oneNetLc.m_mapAttributes["netLcIrCompressMode"].m_strOriginValue = 1.ToString();
            oneNetLc.m_mapAttributes["netLcAntPortNum"].m_strOriginValue = 1.ToString();
            oneNetLc.m_mapAttributes["netLcFrameType"].m_strOriginValue = 1.ToString();
            oneNetLc.m_mapAttributes["netLcCellCombineEnhancedSwitch"].m_strOriginValue = 0.ToString();
            oneNetLc.m_mapAttributes["netLcSdcFuncSwitch"].m_strOriginValue = 1.ToString();
            return oneNetLc;
        }

        private DevAttributeInfo SimAddBoardRru(string index)
        {
            DevAttributeInfo oneIrOfp = new DevAttributeInfo(EnumDevType.board_rru, index);
            oneIrOfp.m_mapAttributes["netIROfpPortRowStatus"].m_strLatestValue = 4.ToString();
            oneIrOfp.m_mapAttributes["netIROfpTransPlanSpeed"].m_strLatestValue = 3.ToString();
            return oneIrOfp;
        }

        private MAP_DEVTYPE_DEVATTRI SimGetNetPlanEnbMib()
        {
            MAP_DEVTYPE_DEVATTRI enbCurPlan = new MAP_DEVTYPE_DEVATTRI();
            //4槽位网规了RRU318FA，上面有两个小区1,2
            DevAttributeInfo oneBoard = new DevAttributeInfo(EnumDevType.board, ".0.0.4");
            oneBoard.m_mapAttributes["netBoardRowStatus"].m_strLatestValue = 4.ToString();
            oneBoard.m_mapAttributes["netBoardType"].m_strLatestValue = 241.ToString();
            oneBoard.m_mapAttributes["netBoardWorkMode"].m_strLatestValue = 2.ToString();
            oneBoard.m_mapAttributes["netBoardIrFrameType"].m_strLatestValue = 1.ToString();
            oneBoard.m_mapAttributes["netBoardRowStatus"].m_strOriginValue = 6.ToString();
            oneBoard.m_mapAttributes["netBoardType"].m_strOriginValue = 178.ToString();
            oneBoard.m_mapAttributes["netBoardWorkMode"].m_strOriginValue = 1.ToString();
            oneBoard.m_mapAttributes["netBoardIrFrameType"].m_strOriginValue = 2.ToString();
            List<DevAttributeInfo> netBoardInfo = new List<DevAttributeInfo>();
            netBoardInfo.Add(oneBoard);
            oneBoard = new DevAttributeInfo(EnumDevType.board, ".0.0.1");
            oneBoard.m_mapAttributes["netBoardRowStatus"].m_strLatestValue = 4.ToString();
            oneBoard.m_mapAttributes["netBoardType"].m_strLatestValue = 173.ToString();
            oneBoard.m_mapAttributes["netBoardWorkMode"].m_strLatestValue = 2.ToString();
            oneBoard.m_mapAttributes["netBoardIrFrameType"].m_strLatestValue = 1.ToString();
            oneBoard.m_mapAttributes["netBoardRowStatus"].m_strOriginValue = 6.ToString();
            oneBoard.m_mapAttributes["netBoardType"].m_strOriginValue = 178.ToString();
            oneBoard.m_mapAttributes["netBoardWorkMode"].m_strOriginValue = 2.ToString();
            oneBoard.m_mapAttributes["netBoardIrFrameType"].m_strOriginValue = 2.ToString();
            netBoardInfo.Add(oneBoard);

            DevAttributeInfo oneRru = new DevAttributeInfo(EnumDevType.rru, ".0");
            oneRru.m_mapAttributes["netRRURowStatus"].m_strLatestValue = 4.ToString();
            oneRru.m_mapAttributes["netRRUManufacturerIndex"].m_strLatestValue = 4.ToString();
            oneRru.m_mapAttributes["netRRUTypeIndex"].m_strLatestValue = 196.ToString();
            oneRru.m_mapAttributes["netRRUOfpWorkMode"].m_strLatestValue = 4.ToString(); //负荷
            oneRru.m_mapAttributes["netRRUAccessRackNo"].m_strLatestValue = 0.ToString();
            oneRru.m_mapAttributes["netRRUAccessShelfNo"].m_strLatestValue = 0.ToString();
            oneRru.m_mapAttributes["netRRUAccessBoardType"].m_strLatestValue = 241.ToString();
            oneRru.m_mapAttributes["netRRUOfp1AccessOfpPortNo"].m_strLatestValue = 0.ToString();
            oneRru.m_mapAttributes["netRRUOfp1AccessLinePosition"].m_strLatestValue = 1.ToString();
            oneRru.m_mapAttributes["netRRUOfp2AccessOfpPortNo"].m_strLatestValue = 3.ToString();
            oneRru.m_mapAttributes["netRRUOfp2AccessLinePosition"].m_strLatestValue = 1.ToString();
            oneRru.m_mapAttributes["netRRUHubNo"].m_strLatestValue = "-1";
            oneRru.m_mapAttributes["netRRUOfp1AccessEthernetPort"].m_strLatestValue = "-1";
            oneRru.m_mapAttributes["netRRUOfp2AccessEthernetPort"].m_strLatestValue = "-1";
            oneRru.m_mapAttributes["netRRUGsmSwitch"].m_strLatestValue = "0";
            List<DevAttributeInfo> netRruInfo = new List<DevAttributeInfo>();
            netRruInfo.Add(oneRru);

            DevAttributeInfo oneAnt = new DevAttributeInfo(EnumDevType.ant, ".1");
            oneAnt.m_mapAttributes["netAntArrayRowStatus"].m_strLatestValue = 4.ToString();
            oneAnt.m_mapAttributes["netAntArrayVendorIndex"].m_strLatestValue = 17.ToString();
            oneAnt.m_mapAttributes["netAntArrayTypeIndex"].m_strLatestValue = 1.ToString();
            oneAnt.m_mapAttributes["netAntArrayHalfPowerBeamWidth"].m_strLatestValue = 65.ToString();
            List<DevAttributeInfo> netAntInfo = new List<DevAttributeInfo>();
            netAntInfo.Add(oneAnt);

            List<DevAttributeInfo> netRruAntInfo = new List<DevAttributeInfo>();
            DevAttributeInfo oneRruAnt = new DevAttributeInfo(EnumDevType.rru_ant, ".0.0");
            oneRruAnt.m_mapAttributes["netSetRRUPortWithAntennaRowStatus"].m_strLatestValue = 4.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortAntArrayNo"].m_strLatestValue = 1.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortAntArrayPathNo"].m_strLatestValue = 1.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortSubtoLocalCellId"].m_strLatestValue = 1.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortSubtoLocalCellId2"].m_strLatestValue = 2.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortTxRxStatus"].m_strLatestValue = 2.ToString();
            netRruAntInfo.Add(oneRruAnt);
            oneRruAnt = new DevAttributeInfo(EnumDevType.rru_ant, ".0.1");
            oneRruAnt.m_mapAttributes["netSetRRUPortWithAntennaRowStatus"].m_strLatestValue = 4.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortAntArrayNo"].m_strLatestValue = 1.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortAntArrayPathNo"].m_strLatestValue = 1.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortSubtoLocalCellId"].m_strLatestValue = 1.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortSubtoLocalCellId2"].m_strLatestValue = 2.ToString();
            oneRruAnt.m_mapAttributes["netSetRRUPortTxRxStatus"].m_strLatestValue = 3.ToString();
            netAntInfo.Add(oneAnt);

            List<DevAttributeInfo> netIrOfpInfo = new List<DevAttributeInfo>();
            DevAttributeInfo oneIrOfp = new DevAttributeInfo(EnumDevType.board_rru, ".0.0.4.0");
            oneIrOfp.m_mapAttributes["netIROfpPortRowStatus"].m_strLatestValue = 4.ToString();
            oneIrOfp.m_mapAttributes["netIROfpTransPlanSpeed"].m_strLatestValue = 3.ToString();
            netIrOfpInfo.Add(oneIrOfp);
            oneIrOfp = new DevAttributeInfo(EnumDevType.board_rru, ".0.0.4.3");
            oneIrOfp.m_mapAttributes["netIROfpPortRowStatus"].m_strLatestValue = 4.ToString();
            oneIrOfp.m_mapAttributes["netIROfpTransPlanSpeed"].m_strLatestValue = 3.ToString();
            netIrOfpInfo.Add(oneIrOfp);

            List<DevAttributeInfo> netLcCtrInfo = new List<DevAttributeInfo>();
            DevAttributeInfo oneNetLcCtr = new DevAttributeInfo(EnumDevType.nrNetLcCtr, ".1");
            oneNetLcCtr.m_mapAttributes["nrNetLocalCellCtrlConfigSwitch"].m_strLatestValue = 0.ToString();
            netLcCtrInfo.Add(oneNetLcCtr);
            oneNetLcCtr = new DevAttributeInfo(EnumDevType.nrNetLcCtr, ".2");
            oneNetLcCtr.m_mapAttributes["nrNetLocalCellCtrlConfigSwitch"].m_strLatestValue = 0.ToString();
            netLcCtrInfo.Add(oneNetLcCtr);

            List<DevAttributeInfo> netLcInfo = new List<DevAttributeInfo>();
            DevAttributeInfo oneNetLc = new DevAttributeInfo(EnumDevType.nrNetLc, ".1");
            oneNetLc.m_mapAttributes["nrNetLocalCellRowStatus"].m_strLatestValue = 4.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellFreqBand"].m_strLatestValue = 8192.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellFreqBandWidth"].m_strLatestValue = 3.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellAntCombinationFlag"].m_strLatestValue = 0.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellAppScene"].m_strLatestValue = "0";
            oneNetLc.m_mapAttributes["nrNetLocalCellAntArrayMode"].m_strLatestValue = 1.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellIrCompressMode"].m_strLatestValue = 1.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellAntPortNum"].m_strLatestValue = 1.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellFrameType"].m_strLatestValue = 1.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellCombineEnhancedSwitch"].m_strLatestValue = 0.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellSdcFuncSwitch"].m_strLatestValue = 1.ToString();
            netLcInfo.Add(oneNetLc);
            oneNetLc = new DevAttributeInfo(EnumDevType.nrNetLc, ".2");
            oneNetLc.m_mapAttributes["nrNetLocalCellRowStatus"].m_strLatestValue = 4.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellFreqBand"].m_strLatestValue = 8192.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellFreqBandWidth"].m_strLatestValue = 3.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellAntCombinationFlag"].m_strLatestValue = 0.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellAppScene"].m_strLatestValue = "0";
            oneNetLc.m_mapAttributes["nrNetLocalCellAntArrayMode"].m_strLatestValue = 1.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellIrCompressMode"].m_strLatestValue = 1.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellAntPortNum"].m_strLatestValue = 1.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellFrameType"].m_strLatestValue = 1.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellCombineEnhancedSwitch"].m_strLatestValue = 0.ToString();
            oneNetLc.m_mapAttributes["nrNetLocalCellSdcFuncSwitch"].m_strLatestValue = 1.ToString();
            netLcInfo.Add(oneNetLc);

            enbCurPlan.Add(EnumDevType.board, netBoardInfo);
            enbCurPlan.Add(EnumDevType.rru, netRruInfo);
            enbCurPlan.Add(EnumDevType.ant, netAntInfo);
            enbCurPlan.Add(EnumDevType.rru_ant, netRruAntInfo);
            enbCurPlan.Add(EnumDevType.board_rru, netIrOfpInfo);
            enbCurPlan.Add(EnumDevType.nrNetLc, netLcInfo);
            enbCurPlan.Add(EnumDevType.nrNetLcCtr, netLcCtrInfo);
            return enbCurPlan;
        }

        private Shelf SimSetLibShelf(int equipNEType, int slotNum, string boardtype)
        {
            Shelf shelf = new Shelf();
            shelf.number = 2;
            shelf.equipNEType = equipNEType;
            shelf.totalSlotNum = 13;
            shelf.supportPlanSlotNum = slotNum;
            shelf.columnsUI = 2;
            if (10 == equipNEType)
            {
                shelf.equipNETypeName = "emb6116|EMB6116";
            }
            else
            {
                shelf.equipNETypeName = "emb5116|EMB5116";
            }
            List<ShelfSlotInfo> listInfo = new List<ShelfSlotInfo>();
            string[] split = boardtype.Split(',');
            for (int loop = 0; loop < split.Length && loop < slotNum; loop++)
            {
                ShelfSlotInfo attInfo = new ShelfSlotInfo();
                attInfo.slotIndex = loop;
                VD vd = new VD();
                vd.desc = split[loop] + "板";
                vd.value = split[loop];
                attInfo.supportBoardType.Add(vd);
                listInfo.Add(attInfo);
            }
                        
            shelf.planSlotInfo = listInfo;
            return shelf;
        }

        [TestMethod()]
        public void NPECheckRulesHelperTest()
        {
        }

        [TestMethod()]
        public async Task IsValidParaNameTest()
        {
            await simConnectEnb();
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");

            string[] name =
            {
                "this.aa", "mib.bb", "cur.dd", "old.ff", "lib.ss", "query3", "query4.bb", "it.rowStatus",
                "board.rowStatus", "query.test"
            };
            bool[] realRes = { true, true, true, true, true, true, true, true, false, false, false };
            int loop = 0;
            foreach (var tmp in name)
            {
                bool res = rulesHelper.IsValidParaName(tmp);
                Assert.IsTrue(realRes[loop] == res);
                loop++;
            }
        }

        [TestMethod()]
        public async Task GetPropertyConditionValueTest()
        {
            await simConnectEnb();
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            //校验前提校验
            string property = "where cur.netBoardEntry.netBoardRowStatus != old.netBoardEntry.netBoardRowStatus";
            DevAttributeInfo curBoard = SimAddBoard(".0.0.4", "241", "4");
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            EnumResultType res = rulesHelper.GetPropertyConditionValue(property, curBoard);
            Assert.IsTrue(EnumResultType.success_true == res);

            //查询不到该变量
            property = "where cur.netBoardEntry.netBoardRowStatusex == netBoardEntry.netBoardRowStatus";
            res = rulesHelper.GetPropertyConditionValue(property, curBoard);
            Assert.IsTrue(EnumResultType.fail_continue == res);

            //值为字符串
            property = "where cur.netBoardEntry.netBoardRowStatus == netBoardEntry.netBoardRowStatus";
            res = rulesHelper.GetPropertyConditionValue(property, curBoard);
            Assert.IsTrue(EnumResultType.success_false == res);

            //多种条件组合
            property =
                "where cur.netBoardEntry.netBoardRowStatus != 4 && cur.netBoardEntry.netBoardType == 241 && cur.netBoardEntry.netBoardWorkMode ！= 2";
            res = rulesHelper.GetPropertyConditionValue(property, curBoard);
            Assert.IsTrue(EnumResultType.success_false == res);
        }

        [TestMethod()]
        public async Task MapQueryAndData4LayerTest()
        {
            await simConnectEnb();
            string preQueryName = "lib.boardEquipment.irOfpPortInfo.irOfpPortTransSpeed";
            List<VD> queryValue = new List<VD>();
            VD vd = new VD();
            vd.value = "1";
            vd.desc = "mbps5000|5G";
            queryValue.Add(vd);
            vd = new VD();
            vd.value = "0";
            vd.desc = "mbps2500|2.5G";

            queryValue.Add(vd);
            object tmp = queryValue;
            string leafName = "value";
            object objValue;
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            EnumResultType res = rulesHelper.MapQueryAndData4Layer(preQueryName, tmp, leafName, out objValue);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(objValue.ToString().Equals("1"));

            preQueryName = "lib.boardEquipment.irOfpPortInfo.irOfpPortTransTxSpeed";
            res = rulesHelper.MapQueryAndData4Layer(preQueryName, tmp, leafName, out objValue);
            Assert.IsTrue(res == EnumResultType.success_true);

            preQueryName = "lib.boardEquipment.irOfpPortInfo.irOfpPortTransTxSpeed";
            leafName = "testV";
            res = rulesHelper.MapQueryAndData4Layer(preQueryName, tmp, leafName, out objValue);
            Assert.IsTrue(res == EnumResultType.fail_continue);
        }

        [TestMethod()]
        public async Task MapLibQueryOfDataTypeTest()
        {
            await simConnectEnb();
            string preQueryName = "lib.rruTypeInfo";
            List<RruInfo> rruInfoList = new List<RruInfo>();
            RruInfo rruInfo = new RruInfo();
            rruInfo.rruTypeIndex = 227;
            rruInfo.rruTypeName = "TDRU332FA-E";
            rruInfo.rruTypeManufacturerIndex = 4;
            rruInfo.rruTypeBandWidth = 40;
            VD vd = new VD();
            vd.desc = "td|TD-SCDMA";
            vd.value = "2";
            rruInfo.rruTypeSupportCellWorkMode.Add(vd);
            vd = new VD();
            vd.desc = "lte|LTE TDD";
            vd.value = "1";
            rruInfo.rruTypeSupportCellWorkMode.Add(vd);
            rruInfoList.Add(rruInfo);
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            object objValue;
            EnumResultType res = rulesHelper.MapLibQueryOfDataType(preQueryName, rruInfoList,
                "rruTypeSupportCellWorkMode", out objValue);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(objValue is List<VD>);

            res = rulesHelper.MapLibQueryOfDataType(preQueryName, rruInfoList, "rruTypeName", out objValue);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(objValue is string);
            Assert.IsTrue(objValue.ToString().Equals("TDRU332FA-E"));

            res = rulesHelper.MapLibQueryOfDataType(preQueryName, rruInfoList, "bandwidth", out objValue);
            Assert.IsTrue(res == EnumResultType.fail_continue);
        }

        [TestMethod()]
        public async Task MapLibQueryOfDataTypeTest1()
        {
            await simConnectEnb();
            Shelf shelf = new Shelf();
            shelf.number = 2;
            shelf.equipNEType = 10;
            shelf.totalSlotNum = 13;
            shelf.supportPlanSlotNum = 12;
            shelf.columnsUI = 2;

            List<ShelfSlotInfo> listInfo = new List<ShelfSlotInfo>();
            ShelfSlotInfo attInfo = new ShelfSlotInfo();
            attInfo.slotIndex = 0;
            VD vd = new VD();
            vd.desc = "hsctd|HSCTD板";
            vd.value = "23";
            attInfo.supportBoardType.Add(vd);
            listInfo.Add(attInfo);
            shelf.planSlotInfo.Add(attInfo);
            attInfo = new ShelfSlotInfo();
            attInfo.slotIndex = 1;
            vd = new VD();
            vd.desc = "hsctd|HSCTD板";
            vd.value = "23";
            attInfo.supportBoardType.Add(vd);
            listInfo.Add(attInfo);
            shelf.planSlotInfo.Add(attInfo);
            attInfo = new ShelfSlotInfo();
            attInfo.slotIndex = 2;
            listInfo.Add(attInfo);
            shelf.planSlotInfo.Add(attInfo);
            List<Shelf> shelfList = new List<Shelf>();
            shelfList.Add(shelf);

            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            object objValue;
            string preQueryName = "lib.shelfEquipment";
            string leafName = "equipNEType";
            EnumResultType res = rulesHelper.MapLibQueryOfDataType(preQueryName, shelfList, leafName, out objValue);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(objValue is String);
            Assert.IsTrue("10" == Convert.ToString(objValue));

            preQueryName = "lib.shelfEquipment.planSlotInfo";
            leafName = "supportBoardType";
            res = rulesHelper.MapLibQueryOfDataType(preQueryName, listInfo, leafName, out objValue);
            Assert.IsTrue(res == EnumResultType.success_true);
        }

        [TestMethod()]
        public async Task GetQueryFatherNameTest()
        {
            await simConnectEnb();
            Shelf shelf = SimSetLibShelf(10, 12, "150,143,243");
            List<Shelf> shelfList = new List<Shelf>();
            shelfList.Add(shelf);
            shelf = SimSetLibShelf(5, 8, "171,172,173,178");
            shelfList.Add(shelf);
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            //var list1 = shelfList.AsQueryable().Where("equipNEType == 5", null).Select("it");
            queryDic.Add("query11.lib.shelfEquipment.planSlotInfo", shelfList[1].planSlotInfo);
            queryDic.Add("query1.lib.shelfEquipment", shelfList);
            queryDic.Add("query2.lib.shelfEquipment.planSlotInfo", shelfList[0].planSlotInfo);
            queryDic.Add("query3.lib.shelfEquipment.planSlotInfo.supportBoardType",
                shelfList[0].planSlotInfo[0].supportBoardType);

            string expr = "query1.equipNETypeName";
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            string fatherName;
            bool res = rulesHelper.GetQueryFatherName(expr, queryDic, out fatherName);
            Assert.IsTrue(res == true);
            Assert.IsTrue(fatherName.Equals("query1.lib.shelfEquipment"));

            expr = "query1";
            res = rulesHelper.GetQueryFatherName(expr, queryDic, out fatherName);
            Assert.IsTrue(res == true);
            Assert.IsTrue(fatherName.Equals("query1.lib.shelfEquipment"));

            expr = "query11";
            res = rulesHelper.GetQueryFatherName(expr, queryDic, out fatherName);
            Assert.IsTrue(res == true);
            Assert.IsTrue(fatherName.Equals("query11.lib.shelfEquipment.planSlotInfo"));

            expr = "query2.supportBoardType";
            res = rulesHelper.GetQueryFatherName(expr, queryDic, out fatherName);
            Assert.IsTrue(res == true);
            Assert.IsTrue(fatherName.Equals("query2.lib.shelfEquipment.planSlotInfo"));

            expr = "query3.value";
            res = rulesHelper.GetQueryFatherName(expr, queryDic, out fatherName);
            Assert.IsTrue(res == true);
            Assert.IsTrue(fatherName.Equals("query3.lib.shelfEquipment.planSlotInfo.supportBoardType"));

            expr = "query4.value";
            res = rulesHelper.GetQueryFatherName(expr, queryDic, out fatherName);
            Assert.IsTrue(res == false);
        }

        [TestMethod()]
        public async Task ConvertQueryValueTest()
        {
            await simConnectEnb();
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");

            Shelf shelf = SimSetLibShelf(10, 12, "150,143,243");
            List<Shelf> shelfList = new List<Shelf>();
            shelfList.Add(shelf);
            shelf = SimSetLibShelf(5, 8, "171,172,173,178");
            shelfList.Add(shelf);
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            List<Object> paramList = new List<object>();
            paramList.Add(10);
            var list1 = shelfList.AsQueryable().Where("equipNEType == @0", paramList.ToArray()).Select("it");
            queryDic.Add("query1.lib.shelfEquipment", list1);
            //var list2 = shelfList.AsQueryable().Where("equipNEType == @0", paramList.ToArray()).Select("it.planslotInfo");
            queryDic.Add("query2.lib.shelfEquipment.planSlotInfo", shelfList[0].planSlotInfo);
            paramList = new List<object>();
            paramList.Add(0);
            var list3 = shelfList[0].planSlotInfo.AsQueryable().Where("slotIndex == @0", paramList.ToArray())
                .Select("it.supportBoardType");
            //queryDic.Add("query3.lib.shelfEquipment.planSlotInfo.supportBoardType", list3);
            queryDic.Add("query3.lib.shelfEquipment.planSlotInfo.supportBoardType", shelfList[0].planSlotInfo[0].supportBoardType);
            //遍历
            string exptr = "query1";
            object objRes;
            EnumResultType res = rulesHelper.ConvertQueryValue(exptr, queryDic, out objRes);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(objRes is IEnumerable<Shelf>);
            var list4 = shelfList.AsQueryable().Select("it");
            queryDic.Add("query4.lib.shelfEquipment", list4);

            //验证动态linq查询一条记录时返回与多条记录返回结果类型是不一样的??  -- 是一样的
            object obj4;
            queryDic.TryGetValue("query4.lib.shelfEquipment", out obj4);
            Assert.IsTrue(obj4 is IEnumerable<Shelf>);
            object obj2;
            queryDic.TryGetValue("query2.lib.shelfEquipment.planSlotInfo", out obj2);
            Assert.IsTrue(obj2 is List<ShelfSlotInfo>);

            //使用到叶子节点
            exptr = "query2.slotIndex";
            res = rulesHelper.ConvertQueryValue(exptr, queryDic, out objRes);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(0 == Convert.ToInt32(objRes.ToString()));

            //无此节点
            exptr = "query3.slotIndex";
            res = rulesHelper.ConvertQueryValue(exptr, queryDic, out objRes);
            Assert.IsTrue(res == EnumResultType.fail_continue);

            //query为空
            exptr = "";
            res = rulesHelper.ConvertQueryValue(exptr, queryDic, out objRes);
            Assert.IsTrue(res == EnumResultType.fail);
        }

        [TestMethod()]
        public async Task ConvertQueryValueTest1()
        {
            await simConnectEnb();
            //mib，this情况
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");

            Shelf shelf = SimSetLibShelf(10, 12, "150,143,243");
            List<Shelf> shelfList = new List<Shelf>();
            shelfList.Add(shelf);
            shelf = SimSetLibShelf(5, 8, "171,172,173,178");
            shelfList.Add(shelf);
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            queryDic.Add("query1.lib.shelfEquipment", shelfList);
            queryDic.Add("query2.lib.shelfEquipment.planSlotInfo", shelfList[0].planSlotInfo);
            queryDic.Add("query3.lib.shelfEquipment.planSlotInfo.supportBoardType",
                shelfList[0].planSlotInfo[0].supportBoardType);
            //无mib的DIC
            string exptr = "mib.netIROptPlanEntry";
            object objRes;
            EnumResultType res = rulesHelper.ConvertQueryValue(exptr, queryDic, out objRes);
            Assert.IsTrue(res == EnumResultType.fail);
            //查找mib
            List<DevAttributeInfo> netBoardInfo;
            mapMib_this.TryGetValue(EnumDevType.board, out netBoardInfo);
            queryDic.Add("query4.mib.netBoardEntry", netBoardInfo);
            exptr = "query4.netBoardIrFrameType";
            res = rulesHelper.ConvertQueryValue(exptr, queryDic, out objRes);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue("2" == Convert.ToString(objRes));

            queryDic.Add("query5.this.netBoardEntry", mapMib_this.TryGetValue(EnumDevType.board, out netBoardInfo));
            exptr = "query5.netBoardIrFrameType";
            res = rulesHelper.ConvertQueryValue(exptr, queryDic, out objRes);
            Assert.IsTrue(res == EnumResultType.fail);

            queryDic.Add("query6.this.netBoardEntry", netBoardInfo);
            exptr = "query6.netBoardIrFrameType";
            res = rulesHelper.ConvertQueryValue(exptr, queryDic, out objRes);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue("1" == Convert.ToString(objRes));
        }

        [TestMethod()]
        public async Task GetRoundParaValueTest()
        {
            await simConnectEnb();
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            List<DevAttributeInfo> devAttriList;
            mapMib_this.TryGetValue(EnumDevType.board_rru, out devAttriList);
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            List<string> propertyNameList = new List<string>();

            /*from it in mib.netBoardEntry
              where it.netBoardRackNo == cur.netIROptPlanEntry.netIROfpPortRackNo && it.netBoardShelfNo == cur.netIROptPlanEntry.netIROfpPortShelfNo && it.netBoardSlotNo == cur.netIROptPlanEntry.netIROfpPortSlotNo
              select it*/
            //from语句
            propertyNameList.Add("it");
            propertyNameList.Add("mib.netBoardEntry");
            Dictionary<string, Object> paraValueDic;
            EnumResultType res = rulesHelper.GetRoundParaValue(devAttriList[0], queryDic, propertyNameList, out paraValueDic);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(1 == paraValueDic.Count);
            object paraObj;
            Assert.IsTrue(paraValueDic.TryGetValue("mib.netBoardEntry", out paraObj));
            Assert.IsTrue(paraObj is List<DevAttributeInfo>);

            propertyNameList.Clear();
            propertyNameList.Add("it.netBoardRackNo");
            propertyNameList.Add("cur.netIROptPlanEntry.netIROfpPortRackNo");
            propertyNameList.Add("it.netBoardShelfNo");
            propertyNameList.Add("cur.netIROptPlanEntry.netIROfpPortShelfNo");
            propertyNameList.Add("it.netBoardSlotNo");
            propertyNameList.Add("cur.netIROptPlanEntry.netIROfpPortSlotNo");
            res = rulesHelper.GetRoundParaValue(devAttriList[0], queryDic, propertyNameList, out paraValueDic);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(3 == paraValueDic.Count);
            Assert.IsTrue(paraValueDic.TryGetValue("cur.netIROptPlanEntry.netIROfpPortSlotNo", out paraObj));
            Assert.IsTrue("4" == Convert.ToString(paraObj));

            propertyNameList.Clear();
            propertyNameList.Add("it");
            res = rulesHelper.GetRoundParaValue(devAttriList[0], queryDic, propertyNameList, out paraValueDic);
            Assert.IsTrue(0 == paraValueDic.Count);
        }

        [TestMethod()]
        public async Task TakeRoundRulesConditionalResultTest()
        {
            await simConnectEnb();
            //mib，this情况
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");

            Shelf shelf = SimSetLibShelf(10, 12, "150,143,243");
            List<Shelf> shelfList = new List<Shelf>();
            shelfList.Add(shelf);
            shelf = SimSetLibShelf(5, 8, "171,172,173,178");
            shelfList.Add(shelf);
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            queryDic.Add("query1.lib.shelfEquipment", shelfList);
            queryDic.Add("query2.lib.shelfEquipment.planSlotInfo", shelfList[0].planSlotInfo);
            queryDic.Add("query3.lib.shelfEquipment.planSlotInfo.supportBoardType",
                shelfList[0].planSlotInfo[0].supportBoardType);

            //count关键字条件语句
            string expr = "where query3.Count > 0";
            List<NetPlan.DevAttributeInfo> boardList;
            mapMib_this.TryGetValue(EnumDevType.board, out boardList);
            bool finalRes;
            EnumResultType res = rulesHelper.TakeRoundRulesConditionalResult(expr, boardList[0], queryDic, out finalRes);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(finalRes == true);

            //查询lib
            expr = "where query2.slotIndex == 8"; //不存在这个槽位
            res = rulesHelper.TakeRoundRulesConditionalResult(expr, boardList[0], queryDic, out finalRes);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(finalRes == false);

            //查询mib
            queryDic.Add("query4.mib.netBoardEntry", boardList);
            queryDic.Add("query5.this.netBoardEntry", boardList);
            expr = "where query4.netBoardWorkMode == 2";
            res = rulesHelper.TakeRoundRulesConditionalResult(expr, boardList[0], queryDic, out finalRes);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(finalRes == false);

            expr = "where query5.netBoardWorkMode == 2";
            res = rulesHelper.TakeRoundRulesConditionalResult(expr, boardList[0], queryDic, out finalRes);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(finalRes == true);
        }

        [TestMethod()]
        public async Task TakeRoundRulesConditionalResultTest1()
        {
            await simConnectEnb();
            //构造几种计算结果false的场景
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");

            Shelf shelf = SimSetLibShelf(10, 12, "150,143,243");
            List<Shelf> shelfList = new List<Shelf>();
            shelfList.Add(shelf);
            shelf = SimSetLibShelf(5, 8, "171,172,173,178");
            shelfList.Add(shelf);
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            queryDic.Add("query1.lib.shelfEquipment", shelfList);
            queryDic.Add("query2.lib.shelfEquipment.planSlotInfo", shelfList[0].planSlotInfo);
            queryDic.Add("query3.lib.shelfEquipment.planSlotInfo.supportBoardType",
                shelfList[0].planSlotInfo[0].supportBoardType);

            string expr = "from it in mib.netBoardEntry";
            List<NetPlan.DevAttributeInfo> boardList;
            mapMib_this.TryGetValue(EnumDevType.board, out boardList);
            bool finalRes;
            EnumResultType res = rulesHelper.TakeRoundRulesConditionalResult(expr, boardList[0], queryDic, out finalRes);
            Assert.IsTrue(res == EnumResultType.fail);
        }

        [TestMethod()]
        public async Task ConvertNameInWhereTest()
        {
            await simConnectEnb();
            //填写格式存在问题场景
            string fromName = "mib.netBoardEntry";
            string wherePart = "where it. == 4 && it.netBoardType == 241";
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            List<string> whereNameList = new List<string>();
            whereNameList.Add("it.");
            whereNameList.Add("it.netBoardType");
            string whereLast;

            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            bool res = rulesHelper.ConvertNameInWhere(fromName, wherePart, queryDic, whereNameList, out whereLast);
            Assert.IsTrue(res == false);

            wherePart = "where query1.netBoardRowStatus == 4 && it.netBoardType == 241";
            res = rulesHelper.ConvertNameInWhere(fromName, wherePart, queryDic, whereNameList, out whereLast);
            Assert.IsTrue(res == false);
        }

        [TestMethod()]
        public async Task ConvertNameInWhereTest1()
        {
            await simConnectEnb();
            string fromName = "mib.netBoardEntry";
            string wherePart = "where it.netBoardRowStatus == 4 && it.netBoardType == 241";
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            List<string> whereNameList = new List<string>();
            whereNameList.Add("it.netBoardRowStatus");
            whereNameList.Add("it.netBoardType");
            whereNameList.Add("4");
            whereNameList.Add("241");
            string whereLast;

            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            //替换mib中
            bool res = rulesHelper.ConvertNameInWhere(fromName, wherePart, queryDic, whereNameList, out whereLast);
            Assert.IsTrue(res == true);
            Assert.IsTrue(whereLast.Equals(
                @"where it.m_mapAttributes[""netBoardRowStatus""].m_strOriginValue == ""4"" && it.m_mapAttributes[""netBoardType""].m_strOriginValue == ""241"""));

            //替换this.中
            fromName = "this.netBoardEntry";
            wherePart = "where it.netBoardRowStatus == aa.bb && it.netBoardType == cc";
            whereNameList = new List<string>();
            whereNameList.Add("it.netBoardRowStatus");
            whereNameList.Add("it.netBoardType");
            whereNameList.Add("aa.bb");
            whereNameList.Add("cc");
            res = rulesHelper.ConvertNameInWhere(fromName, wherePart, queryDic, whereNameList, out whereLast);
            Assert.IsTrue(res == true);
            Assert.IsTrue(whereLast.Equals(
                @"where it.m_mapAttributes[""netBoardRowStatus""].m_strLatestValue == ""aa.bb"" && it.m_mapAttributes[""netBoardType""].m_strLatestValue == ""cc"""));

            //不需要替换 
            fromName = "lib.rruTypeInfo";
            wherePart = "where it.rruTypeManufacturerIndex == 4 && it.rruTypeIndex == 127";
            whereNameList = new List<string>();
            whereNameList.Add("it.rruTypeManufacturerIndex");
            whereNameList.Add("it.rruTypeIndex");
            res = rulesHelper.ConvertNameInWhere(fromName, wherePart, queryDic, whereNameList, out whereLast);
            Assert.IsTrue(res == true);
            Assert.IsTrue(whereLast.Equals(@"where it.rruTypeManufacturerIndex == 4 && it.rruTypeIndex == 127"));

            //存在query
            List<NetPlan.DevAttributeInfo> boardList;
            mapMib_this.TryGetValue(EnumDevType.board, out boardList);
            queryDic.Add("query1.this.netBoardEntry", boardList);
            fromName = "lib.rruTypeInfo";
            wherePart = "where query1.netBoardType == abc && it.rruTypeIndex == abcd";
            whereNameList = new List<string>();
            whereNameList.Add("query1.netBoardType");
            whereNameList.Add("abc");
            whereNameList.Add("it.rruTypeIndex");
            whereNameList.Add("abcd");
            res = rulesHelper.ConvertNameInWhere(fromName, wherePart, queryDic, whereNameList, out whereLast);
            Assert.IsTrue(res == true);
            Assert.IsTrue(whereLast.Equals(@"where query1.netBoardType == ""abc"" && it.rruTypeIndex == ""abcd"""));

            queryDic.Add("query2.mib.netBoardEntry", boardList);
            fromName = "query2";
            wherePart = "where it.netBoardRowStatus == 4 && it.netBoardType == 241";
            whereNameList = new List<string>();
            whereNameList.Add("it.netBoardRowStatus");
            whereNameList.Add("it.netBoardType");
            res = rulesHelper.ConvertNameInWhere(fromName, wherePart, queryDic, whereNameList, out whereLast);
            Assert.IsTrue(res == true);
            Assert.IsTrue(whereLast.Equals(
                @"where it.m_mapAttributes[""netBoardRowStatus""].m_strOriginValue == 4 && it.m_mapAttributes[""netBoardType""].m_strOriginValue == 241"));

            List<string> boardTypeList = new List<string> { "241", "173", "207" };
            queryDic.Add("query3.mib.netBoardEntry.netBoardType", boardTypeList);
            fromName = "query3";
            whereNameList = new List<string>();
            whereNameList.Add("it.Count");
            wherePart = "where it.Count > 0";
            res = rulesHelper.ConvertNameInWhere(fromName, wherePart, queryDic, whereNameList, out whereLast);
            Assert.IsTrue(res == true);
            Assert.IsTrue(whereLast.Equals("where it.Count > 0"));

            fromName = "this.netBoardEntry";
            wherePart = "where it. == 2";
            whereNameList = new List<string>();
            whereNameList.Add("it.");
            res = rulesHelper.ConvertNameInWhere(fromName, wherePart, queryDic, whereNameList, out whereLast);
            Assert.IsTrue(res == false);
        }

        [TestMethod()]
        public async Task IsMibTableTest()
        {
            await simConnectEnb();
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");

            string name = "mib.cellEntry";
            bool res = rulesHelper.IsMibTable(name);
            Assert.IsTrue(res == true);

            name = "this.cellEntry";
            res = rulesHelper.IsMibTable(name);
            Assert.IsTrue(res == true);

            name = "this.cellEntry.cellRowStatus";
            res = rulesHelper.IsMibTable(name);
            Assert.IsTrue(res == false);

            name = "mib.cellEntry.cellRowStatus";
            res = rulesHelper.IsMibTable(name);
            Assert.IsTrue(res == false);

            name = "query.cellEntry";
            res = rulesHelper.IsMibTable(name);
            Assert.IsTrue(res == false);

            name = "this.";
            res = rulesHelper.IsMibTable(name);
            Assert.IsTrue(res == false);

            name = "mib.";
            res = rulesHelper.IsMibTable(name);
            Assert.IsTrue(res == false);

        }

        [TestMethod()]
        public async Task IsMibTableOfQueryNameTest()
        {
            await simConnectEnb();
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");

            List<NetPlan.DevAttributeInfo> boardList;
            mapMib_this.TryGetValue(EnumDevType.board, out boardList);

            string queryName = "query1";
            string realName;
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            queryDic.Add("query1.mib.netBoardEntry", boardList);
            List<string> boardTypeList = new List<string> { "241", "173", "207" };
            queryDic.Add("query2.mib.netBoardEntry.netBoardType", boardTypeList);

            bool res = rulesHelper.IsMibTableOfQueryName(queryName, queryDic, out realName);
            Assert.IsTrue(res == true);

            queryName = "query2";
            res = rulesHelper.IsMibTableOfQueryName(queryName, queryDic, out realName);
            Assert.IsTrue(res == false);

            queryName = "query3";
            res = rulesHelper.IsMibTableOfQueryName(queryName, queryDic, out realName);
            Assert.IsTrue(res == false);

            queryName = "anyway.it";
            res = rulesHelper.IsMibTableOfQueryName(queryName, queryDic, out realName);
            Assert.IsTrue(res == false);
        }

        [TestMethod()]
        public async Task ConvertItNameInSelectTest()
        {
            await simConnectEnb();
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            List<NetPlan.DevAttributeInfo> boardList;
            mapMib_this.TryGetValue(EnumDevType.board, out boardList);
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            queryDic.Add("query1.mib.netBoardEntry", boardList);
            List<string> boardTypeList = new List<string> { "241", "173", "207" };
            queryDic.Add("query2.mib.netBoardEntry.netBoardType", boardTypeList);

            string fromName = "this.netBoardEntry";
            string itName = "it";
            string last;
            bool res = rulesHelper.ConvertItNameInSelect(fromName, itName, queryDic, out last);
            Assert.IsTrue(res == true);
            Assert.IsTrue(last.Equals(itName));

            fromName = "this.netBoardEntry";
            itName = "it.netBoardRowStatus";
            res = rulesHelper.ConvertItNameInSelect(fromName, itName, queryDic, out last);
            Assert.IsTrue(res == true);
            Assert.IsTrue(last.Equals(@"it.m_mapAttributes[""netBoardRowStatus""].m_strLatestValue"));

            fromName = "mib.netBoardEntry";
            itName = "it.netBoardRowStatus";
            res = rulesHelper.ConvertItNameInSelect(fromName, itName, queryDic, out last);
            Assert.IsTrue(res == true);
            Assert.IsTrue(last.Equals(@"it.m_mapAttributes[""netBoardRowStatus""].m_strOriginValue"));

            fromName = "lib.netBoardEntry";
            res = rulesHelper.ConvertItNameInSelect(fromName, itName, queryDic, out last);
            Assert.IsTrue(res == true);
            Assert.IsTrue(last.Equals(itName));

            fromName = "mib.netBoardEntry";
            itName = "it.";
            res = rulesHelper.ConvertItNameInSelect(fromName, itName, queryDic, out last);
            Assert.IsTrue(res == false);
        }

        [TestMethod()]
        public async Task AddQueryDicNameTest()
        {
            await simConnectEnb();
            //成功--原始UI数据,mib数据
            string outvar = "query1";
            string fatherName = "this.netBoardEntry";
            string strSelect = "it";
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            List<NetPlan.DevAttributeInfo> boardList;
            mapMib_this.TryGetValue(EnumDevType.board, out boardList);
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            bool res = rulesHelper.AddQueryDicName(outvar, fatherName, boardList, strSelect, queryDic);
            Assert.IsTrue(res == true && queryDic.Count == 1);
            object newQ;
            Assert.IsTrue(queryDic.TryGetValue("query1.this.netBoardEntry", out newQ));
            Assert.IsTrue(newQ is List<DevAttributeInfo>);

            outvar = "query2";
            fatherName = "mib.netBoardEntry";
            res = rulesHelper.AddQueryDicName(outvar, fatherName, boardList, strSelect, queryDic);
            Assert.IsTrue(res == true && queryDic.Count == 2);
            Assert.IsTrue(queryDic.TryGetValue("query2.mib.netBoardEntry", out newQ));
            Assert.IsTrue(newQ is List<DevAttributeInfo>);

            //成功--原始lib数据
            outvar = "query3";
            List<RruInfo> rruLib = rulesHelper.getDataLib()._rruTypeInfo_lib;
            fatherName = "lib.rruTypeInfoEntry";
            res = rulesHelper.AddQueryDicName(outvar, fatherName, rruLib, strSelect, queryDic);
            Assert.IsTrue(res == true && queryDic.Count == 3);
            Assert.IsTrue(queryDic.TryGetValue("query3.lib.rruTypeInfoEntry", out newQ));
            Assert.IsTrue(newQ is List<RruInfo>);
        }

        [TestMethod()]
        public async Task AddQueryDicNameTest1()
        {
            //select中会返回it.XX
            string outvar = "query1";
            string fatherName = "this.netBoardEntry";
            string strSelect = "it.boardType";
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            List<NetPlan.DevAttributeInfo> boardList;
            mapMib_this.TryGetValue(EnumDevType.board, out boardList);
            List<string> boarTypeList = new List<string> { "173", "241", "258" };
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            bool res = rulesHelper.AddQueryDicName(outvar, fatherName, boarTypeList, strSelect, queryDic);
            Assert.IsTrue(res == true && queryDic.Count == 1);
            object newQ;
            Assert.IsTrue(queryDic.TryGetValue("query1.this.netBoardEntry.boardType", out newQ));
            Assert.IsTrue(newQ is List<string>);

            Shelf shelf = SimSetLibShelf(10, 12, "150,143,243");
            List<Shelf> shelfList = new List<Shelf>();
            shelfList.Add(shelf);
            shelf = SimSetLibShelf(5, 8, "171,172,173,178");
            shelfList.Add(shelf);
            fatherName = "lib.shelfEquipment";
            strSelect = "it";
            outvar = "query2";
            var list1 = shelfList.AsQueryable().Select("it");
            res = rulesHelper.AddQueryDicName(outvar, fatherName, list1, strSelect, queryDic);
            Assert.IsTrue(res == true && queryDic.Count == 2);
            object obj;
            Assert.IsTrue(queryDic.TryGetValue("query2.lib.shelfEquipment", out obj));
            Assert.IsTrue(obj is EnumerableQuery<Shelf>);

            fatherName = "lib.shelfEquipment";
            strSelect = "it.equipNEType";
            outvar = "query3";
            var list2 = shelfList.AsQueryable().Select("it.equipNEType");
            res = rulesHelper.AddQueryDicName(outvar, fatherName, list2, strSelect, queryDic);
            Assert.IsTrue(res == true && queryDic.Count == 3);
            object obj2;
            Assert.IsTrue(queryDic.TryGetValue("query3.lib.shelfEquipment.equipNEType", out obj2));
            Assert.IsTrue(obj2 is EnumerableQuery<int>);

            fatherName = "lib.shelfEquipment";
            strSelect = "it.planSlotInfo";
            outvar = "query3";
            var list3 = shelfList.AsQueryable().Select("it.planSlotInfo");
            res = rulesHelper.AddQueryDicName(outvar, fatherName, list3, strSelect, queryDic);
            Assert.IsTrue(res == true && queryDic.Count == 4);
            object obj3;
            Assert.IsTrue(queryDic.TryGetValue("query3.lib.shelfEquipment.planSlotInfo", out obj3));
            Assert.IsTrue(obj3 is List<ShelfSlotInfo>);
        }

        [TestMethod()]
        public async Task AddQueryDicNameTest2()
        {
            await simConnectEnb();
            //查询集合为query变量
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            List<NetPlan.DevAttributeInfo> boardList;
            mapMib_this.TryGetValue(EnumDevType.board, out boardList);

            string outvar = "query2";
            string fatherName = "query1";
            string strSelect = "it";
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            queryDic.Add("query1.this.netBoardEntry", boardList);
            bool res = rulesHelper.AddQueryDicName(outvar, fatherName, boardList, strSelect, queryDic);
            Assert.IsTrue(res == true && queryDic.Count == 2);
            object newQ;
            Assert.IsTrue(queryDic.TryGetValue("query2.this.netBoardEntry", out newQ));
            Assert.IsTrue(newQ is List<DevAttributeInfo>);

            strSelect = "it.workMode";
            outvar = "query3";
            List<string> workList = new List<string> { "2", "1", "3" };
            res = rulesHelper.AddQueryDicName(outvar, fatherName, workList, strSelect, queryDic);
            Assert.IsTrue(res == true && queryDic.Count == 3);
            Assert.IsTrue(queryDic.TryGetValue("query3.this.netBoardEntry.workMode", out newQ));
            Assert.IsTrue(newQ is List<string>);

            outvar = "query5";
            List<RruInfo> rruLib = rulesHelper.getDataLib()._rruTypeInfo_lib;
            queryDic.Add("query4.lib.rruTypeInfoEntry", rruLib);
            fatherName = "query4.rruTypeIrCompressMode";
            strSelect = "it";
            List<VD> compressList = rruLib[0].rruTypeIrCompressMode;
            res = rulesHelper.AddQueryDicName(outvar, fatherName, compressList, strSelect, queryDic);
            Assert.IsTrue(res == true && queryDic.Count == 5);
            Assert.IsTrue(queryDic.TryGetValue("query5.lib.rruTypeInfoEntry.rruTypeIrCompressMode", out newQ));
            Assert.IsTrue(newQ is List<VD>);
        }

        [TestMethod()]
        public async Task AddQueryDicNameTest3()
        {
            await simConnectEnb();
            //outvar非法
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            List<NetPlan.DevAttributeInfo> boardList;
            mapMib_this.TryGetValue(EnumDevType.board, out boardList);
            string outvar = "query";
            string fatherName = "query1";
            string strSelect = "it";
            Dictionary<string, object> queryDic = new Dictionary<string, object>();
            queryDic.Add("query1.this.netBoardEntry", boardList);
            bool res = rulesHelper.AddQueryDicName(outvar, fatherName, boardList, strSelect, queryDic);
            Assert.IsTrue(res == false);

            //queryDic添加重复键值
            outvar = "query1";
            res = rulesHelper.AddQueryDicName(outvar, fatherName, boardList, strSelect, queryDic);
            Assert.IsTrue(res == false);

            //fatherName非法
            fatherName = "query2";
            res = rulesHelper.AddQueryDicName(outvar, fatherName, boardList, strSelect, queryDic);
            Assert.IsTrue(res == false);

            fatherName = "cur.rowStatus";
            res = rulesHelper.AddQueryDicName(outvar, fatherName, boardList, strSelect, queryDic);
            Assert.IsTrue(res == false);
        }

        [TestMethod()]
        public async Task TakeRoundRulesQueryResultTest()
        {
            await simConnectEnb();
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");

            //简单的查询
            RoundRule roundRule = new RoundRule();
            roundRule.round = 1;
            roundRule.rules =
                "from it in lib.rruTypeInfo where it.rruTypeManufacturerIndex == cur.netRRUEntry.netRRUManufacturerIndex && it.rruTypeIndex == cur.netRRUEntry.netRRUTypeIndex select it";
            roundRule.outvar = "query1";
            DevAttributeInfo curRecord = SimAddRru(".1", 133, 4, 1);
            curRecord.m_mapAttributes["netRRURowStatus"].m_strOriginValue = "6";
            curRecord.m_mapAttributes["netRRURowStatus"].m_strLatestValue = "4";
            Dictionary<string, object> queryDic = new Dictionary<string, object>();

            EnumResultType res = rulesHelper.TakeRoundRulesQueryResult(roundRule, curRecord, queryDic);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(queryDic.ContainsKey("query1.lib.rruTypeInfo"));

            object query1res;
            queryDic.TryGetValue("query1.lib.rruTypeInfo", out query1res);
            if (query1res is List<RruInfo> || query1res is RruInfo)
            {
                Assert.IsTrue(true);
            }
            else if (query1res is EnumerableQuery<RruInfo>)
            {
                Assert.IsTrue(true);
            }
            //查询层叠
            roundRule = new RoundRule();
            roundRule.round = 2;
            roundRule.rules =
                "from it in lib.rruTypePortInfo where it.rruTypePortManufacturerIndex == query1.rruTypeManufacturerIndex && it.rruTypePortIndex == query1.rruTypeIndex select it";
            roundRule.outvar = "query2";
            res = rulesHelper.TakeRoundRulesQueryResult(roundRule, curRecord, queryDic);
            Assert.IsTrue(res == EnumResultType.success_true);
            Assert.IsTrue(queryDic.ContainsKey("query2.lib.rruTypePortInfo"));
        }


        [TestMethod()]
        public async Task GetRoundCheckValueTest()
        {
            await simConnectEnb();
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            List<RoundRule> roundList = new List<RoundRule>();
            RoundRule roundRule = new RoundRule();
            roundRule.round = 1;
            roundRule.rules =
                @"where cur.netRRUEntry.netRRUAccessRackNo != -1 && cur.netRRUEntry.netRRUAccessShelfNo != -1 && cur.netRRUEntry.netRRUAccessSlotNo != -1 && cur.netRRUEntry.netRRUOfp1AccessOfpPortNo != -1";
            roundRule.outvar = "";
            roundList.Add(roundRule);

            roundRule = new RoundRule();
            roundRule.round = 2;
            roundRule.rules =
                @"from it in this.netBoardEntry where it.netBoardRackNo == cur.netRRUEntry.netRRUAccessRackNo && it.netBoardShelfNo == cur.netRRUEntry.netRRUAccessShelfNo && it.netBoardSlotNo == cur.netRRUEntry.netRRUAccessSlotNo && it.netBoardRowStatus == 4 select it";
            roundRule.outvar = "query1";
            roundList.Add(roundRule);

            roundRule = new RoundRule();
            roundRule.round = 3;
            roundRule.rules =
                "where query1.Count == 1";
            roundRule.outvar = "";
            roundList.Add(roundRule);
            DevAttributeInfo curRecord = SimAddRru(".1", 133, 5, 1);
            curRecord.m_mapAttributes["netRRURowStatus"].m_strOriginValue = "6";
            curRecord.m_mapAttributes["netRRURowStatus"].m_strLatestValue = "4";
            Dictionary<string, object> queryDic = new Dictionary<string, object>();

            EnumResultType res = rulesHelper.GetRoundCheckValue(roundList, curRecord);
            Assert.IsTrue(res == EnumResultType.success_false);
        }

        [TestMethod()]
        public async Task GetRoundCheckValueTest1()
        {
            await simConnectEnb();
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            List<RoundRule> roundList = new List<RoundRule>();
            RoundRule roundRule = new RoundRule();
            roundRule.round = 1;
            roundRule.rules =
                @"where cur.netRRUEntry.netRRUAccessRackNo != -1 && cur.netRRUEntry.netRRUAccessShelfNo != -1 && cur.netRRUEntry.netRRUAccessSlotNo != -1 && cur.netRRUEntry.netRRUOfp1AccessOfpPortNo != -1";
            roundRule.outvar = "";
            roundList.Add(roundRule);

            roundRule = new RoundRule();
            roundRule.round = 2;
            roundRule.rules =
                @"from it in this.netBoardEntry where it.netBoardRackNo == cur.netRRUEntry.netRRUAccessRackNo && it.netBoardShelfNo == cur.netRRUEntry.netRRUAccessShelfNo && it.netBoardSlotNo == cur.netRRUEntry.netRRUAccessSlotNo && it.netBoardRowStatus == 4 select it";
            roundRule.outvar = "query1";
            roundList.Add(roundRule);

            roundRule = new RoundRule();
            roundRule.round = 3;
            roundRule.rules =
                "where query1.Count == 1";
            roundRule.outvar = "";
            roundList.Add(roundRule);
            DevAttributeInfo curRecord = SimAddRru(".1", 133, 4, 1);
            curRecord.m_mapAttributes["netRRURowStatus"].m_strOriginValue = "6";
            curRecord.m_mapAttributes["netRRURowStatus"].m_strLatestValue = "4";
            Dictionary<string, object> queryDic = new Dictionary<string, object>();

            EnumResultType res = rulesHelper.GetRoundCheckValue(roundList, curRecord);
            Assert.IsTrue(res == EnumResultType.success_true);
        }

        [TestMethod()]
        public async Task GetRoundCheckValueTest2()
        {
            await simConnectEnb();
            MAP_DEVTYPE_DEVATTRI mapMib_this = SimGetNetPlanEnbMib();
            NPECheckRulesHelper rulesHelper = new NPECheckRulesHelper(mapMib_this, "5");
            //测试Contains
            List<RoundRule> roundList = new List<RoundRule>();
            RoundRule roundRule = new RoundRule();
            roundRule.round = 1;
            roundRule.rules =
                @"from it in this.netBoardEntry where it.netBoardRowStatus == 4 select it.netBoardSlotNo";
            roundRule.outvar = "query1";
            roundList.Add(roundRule);

            roundRule = new RoundRule();
            roundRule.round = 2;
            roundRule.rules =
                @"where query1 Contains cur.netRRUEntry.netRRUAccessSlotNo";
            roundRule.outvar = "";
            roundList.Add(roundRule);
            DevAttributeInfo curRecord = SimAddRru(".1", 133, 4, 1);
            curRecord.m_mapAttributes["netRRURowStatus"].m_strOriginValue = "6";
            curRecord.m_mapAttributes["netRRURowStatus"].m_strLatestValue = "4";
            Dictionary<string, object> queryDic = new Dictionary<string, object>();

            EnumResultType res = rulesHelper.GetRoundCheckValue(roundList, curRecord);
            Assert.IsTrue(res == EnumResultType.success_true);
        }
    }
}
