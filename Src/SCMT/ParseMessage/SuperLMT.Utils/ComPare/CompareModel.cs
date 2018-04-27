// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompareModel.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   asn.1 data item
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace SuperLMT.Utils.ComPare
{
    using System.Collections.Generic;
    using System.Windows.Input;

    using DiffMatchPatch;

    /// <summary>
    /// The compare model.
    /// </summary>
    public class CompareModel
    {
        /// <summary>
        /// The to compare command.
        /// </summary>
        private static readonly RoutedCommand ToCompareRoutedCommand = new RoutedCommand();

        /// <summary>
        /// The compare to command.
        /// </summary>
        private static readonly RoutedCommand CompareToRoutedCommandAsn = new RoutedCommand();
        
        /// <summary>
        /// The compare to command.
        /// </summary>
        private static readonly RoutedCommand CompareToRoutedCommandTime = new RoutedCommand();

        /// <summary>
        /// The compare to command.
        /// </summary>
        private static readonly RoutedCommand LoadFilterRouteCommand = new RoutedCommand();

        /// <summary>
        /// The compare to command.
        /// </summary>
        private static readonly RoutedCommand SaveFilterRouteCommand = new RoutedCommand();

        /// <summary>
        /// The compare to command.
        /// </summary>
        private static readonly RoutedCommand DeleteRouteCommand = new RoutedCommand();
        
        /// <summary>
        /// Left selected
        /// The tocompare asn data.
        /// </summary>
        private static string toCompareAsnData = string.Empty;

        /// <summary>
        /// Right selected
        /// The compareto asn data.
        /// </summary>
        private static string compareToAsnData = string.Empty;

        /// <summary>
        /// Gets or sets the to compare asn data.
        /// </summary>
        public static string LeftCompareAsnData
        {
            get
            {
                return toCompareAsnData;
            }

            set
            {
                toCompareAsnData = value;
            }
        }

        /// <summary>
        /// Gets or sets the compare to asn data.
        /// </summary>
        public static string RightCompareToAsnData
        {
            get
            {
                return compareToAsnData;
            }

            set
            {
                compareToAsnData = value;
                Compare(ref toCompareAsnData, ref compareToAsnData);
            }
        }

        /// <summary>
        /// Gets the to compare command.
        /// </summary>
        public static RoutedCommand ToCompareCommand
        {
            get
            {
                return ToCompareRoutedCommand;
            }
        }

        /// <summary>
        /// Gets the compare to command asn.
        /// </summary>
        public static RoutedCommand CompareToCommandAsn
        {
            get
            {
                return CompareToRoutedCommandAsn;
            }
        }
        
        /// <summary>
        /// Gets the compare to command time.
        /// </summary>
        public static RoutedCommand CompareToCommandTime
        {
            get
            {
                return CompareToRoutedCommandTime;
            }
        }

        /// <summary>
        /// Gets the compare to command.
        /// </summary>
        public static RoutedCommand LoadFilterRule
        {
            get
            {
                return LoadFilterRouteCommand;
            }
        }
               
        /// <summary>
        /// Gets the compare to command.
        /// </summary>
        public static RoutedCommand SaveFilterRule
        {
            get
            {
                return SaveFilterRouteCommand;
            }
        }

        /// <summary>
        /// Gets the compare to command.
        /// </summary>
        public static RoutedCommand DeleteEvent
        {
            get
            {
                return DeleteRouteCommand;
            }
        }
        
        /// <summary>
        /// Gets the to compare asn data.
        /// </summary>
        public static IList<AsnDataItem> ToCompareAsnData
        {
            get
            {
                return GenAsnDataItems(toCompareAsnData.Split('\n'), compareToAsnData.Split('\n'));
            }
        }

        /// <summary>
        /// Gets the compare to asn data.
        /// </summary>
        public static IList<AsnDataItem> CompareToAsnData
        {
            get
            {
                return GenAsnDataItems(compareToAsnData.Split('\n'), toCompareAsnData.Split('\n'));
            }
        }

        /// <summary>
        /// The gen asn data items.
        /// </summary>
        /// <param name="asnData1">
        /// The asn data 1.
        /// </param>
        /// <param name="asnData2">
        /// The asn data 2.
        /// </param>
        /// <returns>
        /// The <see>
        ///       <cref>IList</cref>
        ///     </see> .
        /// </returns>
        private static IList<AsnDataItem> GenAsnDataItems(IList<string> asnData1, IList<string> asnData2)
        {
            IList<AsnDataItem> items = new List<AsnDataItem>();
            if (asnData1 == null)
            {
                asnData1 = new List<string>();
            }

            if (asnData2 == null)
            {
                asnData2 = new List<string>();
            }

            for (int index = 0; index < asnData1.Count; index++)
            {
                var item = new AsnDataItem { Index = index, Value = asnData1[index] };
                items.Add(item);
                if (index < asnData2.Count)
                {
                    item.Color = item.Value != asnData2[index] ? "Red" : "Black";
                }
                else
                {
                    item.Color = "Red";
                }
            }

            return items;
        }

        /// <summary>
        /// The compare.
        /// </summary>
        /// <param name="leftString">
        /// The left string.
        /// </param>
        /// <param name="rightString">
        /// The right string.
        /// </param>
        private static void Compare(ref string leftString, ref string rightString)
        {
            var diffMatchPatchWorker = new diff_match_patch();
            var result = diffMatchPatchWorker.diff_linesToChars(leftString, rightString);
            var stringHash = result[2] as List<string>;
            var diffs = diffMatchPatchWorker.diff_main(result[0] as string, result[1] as string, false);

            leftString = string.Empty;
            rightString = string.Empty;

            // white space to patch
            int whiteSpaceCountToPatch = 0;

            // insert delete or delete insert match 
            bool nextDiffMatch = false;

            for (int i = 0; i < diffs.Count; i++)
            {
                var diff = diffs[i];

                char[] indexArray = diff.text.ToCharArray();

                if (diff.operation == Operation.INSERT)
                {
                    if (nextDiffMatch)
                    {
                        AddStringAndPatch(0 - whiteSpaceCountToPatch, ref rightString, ref leftString, indexArray, stringHash);
                        nextDiffMatch = false;
                    }
                    else
                    {
                        whiteSpaceCountToPatch = indexArray.Length;
                        Diff nextDiffer = null;
                        if (i + 1 < diffs.Count)
                        {
                            nextDiffer = diffs[i + 1];
                        }

                        if (null != nextDiffer)
                        {
                            // next if delete
                            if (nextDiffer.operation == Operation.DELETE)
                            {
                                var nextIndexArray = nextDiffer.text.ToCharArray();
                                whiteSpaceCountToPatch = indexArray.Length - nextIndexArray.Length;
                                nextDiffMatch = true;
                            }
                        }

                        AddStringAndPatch(whiteSpaceCountToPatch, ref rightString, ref leftString, indexArray, stringHash);
                    }
                }

                if (diff.operation == Operation.EQUAL)
                {
                    whiteSpaceCountToPatch = 0;

                    foreach (var index in indexArray)
                    {
                        if (stringHash != null)
                        {
                            leftString += stringHash[index];
                            rightString += stringHash[index];
                        }
                    }
                }

                if (diff.operation == Operation.DELETE)
                {
                    if (nextDiffMatch)
                    {
                        AddStringAndPatch(0 - whiteSpaceCountToPatch, ref leftString, ref rightString, indexArray, stringHash);
                        nextDiffMatch = false;
                    }
                    else
                    {
                        whiteSpaceCountToPatch = indexArray.Length;
                        Diff nextDiffer = null;
                        if (i + 1 < diffs.Count)
                        {
                            nextDiffer = diffs[i + 1];
                        }

                        if (null != nextDiffer)
                        {
                            // next if delete
                            if (nextDiffer.operation == Operation.INSERT)
                            {
                                var nextIndexArray = nextDiffer.text.ToCharArray();
                                whiteSpaceCountToPatch = indexArray.Length - nextIndexArray.Length;
                                nextDiffMatch = true;
                            }
                        }

                        AddStringAndPatch(whiteSpaceCountToPatch, ref leftString, ref rightString, indexArray, stringHash);
                    }
                }
            }
        }

        /// <summary>
        /// The add string and patch.
        /// </summary>
        /// <param name="whiteSpaceCountToPatch">
        /// The white space count to patch.
        /// </param>
        /// <param name="stringToAdd">
        /// The string to add.
        /// </param>
        /// <param name="stringToPatch">
        /// The string to patch.
        /// </param>
        /// <param name="indexArray">
        /// The index array.
        /// </param>
        /// <param name="stringHash">
        /// The string hash.
        /// </param>
        private static void AddStringAndPatch(int whiteSpaceCountToPatch, ref string stringToAdd, ref string stringToPatch, char[] indexArray, List<string> stringHash)
        {
            for (int j = 0; j < whiteSpaceCountToPatch; j++)
            {
                stringToPatch += "\n";
            }

            foreach (var index in indexArray)
            {
                if (stringHash != null)
                {
                    stringToAdd += stringHash[index];
                }
            }
        }
    }
}
