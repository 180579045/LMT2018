// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HexMemoryViewer.cs" company="datangmobile">
//   datangmobile
// </copyright>
// <summary>
//   Defines the HexMemoryViewer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SuperLMT.Utils.CustomUI
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Media;

    /// <summary>
    /// The hex memory viewer.
    /// </summary>
    public class HexMemoryViewer : FlowDocumentScrollViewer
    {
        /// <summary>
        /// The selected range property.
        /// </summary>
        public static readonly DependencyProperty SelectedRangeProperty = DependencyProperty.Register(
            "SelectedRange",
            typeof(SelectedRange),
            typeof(HexMemoryViewer),
            new FrameworkPropertyMetadata(
                null, FrameworkPropertyMetadataOptions.None, HandleSelectedRangeChanged));

        /// <summary>
        /// The content property.
        /// </summary>
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(
            "DisplayContent",
            typeof(byte[]),
            typeof(HexMemoryViewer),
            new FrameworkPropertyMetadata(
                null, FrameworkPropertyMetadataOptions.None, HandleContentChanged));

        /// <summary>
        /// The hex table.
        /// </summary>
        private Table hexTable;

        /// <summary>
        /// Gets or sets the selected range.
        /// </summary>
        public SelectedRange SelectedRange
        {
            get
            {
                return (SelectedRange)this.GetValue(SelectedRangeProperty);
            }

            set
            {
                // this.SetCellBackground(this.SelectedRange, Brushes.White);
                this.SetValue(SelectedRangeProperty, value);

                // this.SetCellBackground(this.SelectedRange, Brushes.LightSteelBlue);              
            }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        public byte[] DisplayContent
        {
            get
            {
                return (byte[])this.GetValue(ContentProperty);
            }

            set
            {
                this.SetValue(ContentProperty, value);
                this.DisplayData(value);
            }
        }

        /// <summary>
        /// Gets the hex table.
        /// </summary>
        public Table HexTable
        {
            get
            {
                return this.hexTable ?? (this.hexTable = this.FindName("hexTabel") as Table);
            }
        }

        /// <summary>
        /// The handle content change.
        /// </summary>
        /// <param name="d">
        /// The d.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void HandleContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var hexMemoryViewer = d as HexMemoryViewer;
            if (hexMemoryViewer != null)
            {
                hexMemoryViewer.DisplayData(e.NewValue as byte[]);
            }
        }

        /// <summary>
        /// The handle selected range changed.
        /// </summary>
        /// <param name="d">
        /// The d.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private static void HandleSelectedRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var hexMemoryViewer = d as HexMemoryViewer;
            if (hexMemoryViewer != null)
            {
                var oldRange = e.OldValue as SelectedRange;
                var newRange = e.NewValue as SelectedRange;
                if (null != oldRange)
                {
                    hexMemoryViewer.SetCellBackground(e.OldValue as SelectedRange, Brushes.White);
                }

                if (null != newRange)
                {
                    hexMemoryViewer.SetCellBackground(e.NewValue as SelectedRange, Brushes.LightSteelBlue);   
                }
            }
        }

        /// <summary>
        /// The display data.
        /// </summary>
        /// <param name="memDatas">
        /// The memory data
        /// </param>
        private void DisplayData(byte[] memDatas)
        {
            Table hexTabel = this.HexTable;

            /*清掉原有内容*/
            hexTabel.RowGroups[0].Rows.Clear();

            if (memDatas == null)
            {
                return;
            }
            
            if (hexTabel == null)
            {
                return;
            }
            
            TableRow curRow = null;
            for (int index = 0; index < memDatas.Length; index++)
            {
                if (0 == (index % 16))
                {
                    curRow = new TableRow();
                    hexTabel.RowGroups[0].Rows.Add(curRow);
                    curRow.Cells.Add(new TableCell(new Paragraph(new Run(this.ConvertIntegerToHexAddress(index)))));
                }

                var memCell = new TableCell(new Paragraph(new Run(this.ConvertByteToHexByte(memDatas[index]))))
                    { Name = string.Format(@"memCell{0}", index) };
                if (curRow != null)
                {
                    curRow.Cells.Add(memCell);
                }
            }
        }

        /// <summary>
        /// The set cell background.
        /// </summary>
        /// <param name="dataRange">
        /// The data range.
        /// </param>
        /// <param name="brush">
        /// The background brush.
        /// </param>
        private void SetCellBackground(SelectedRange dataRange, Brush brush)
        {
            for (int index = 0; index < dataRange.DataLength; index++)
            {
                string strCellName = string.Format(@"memCell{0}", dataRange.StartPosition + index);

                TableCell curCell = this.FindTableCellByName(strCellName);
                if (null != curCell)
                {
                    curCell.Background = brush;
                }
            }
        }

        /// <summary>
        /// The find table cell by name.
        /// </summary>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <returns>
        /// The <see cref="TableCell"/>.
        /// </returns>
        private TableCell FindTableCellByName(string cellName)
        {
            return this.HexTable.RowGroups[0].Rows.SelectMany(curRow => curRow.Cells).FirstOrDefault(curCell => 0 == string.CompareOrdinal(cellName, curCell.Name));
        }

        /// <summary>
        /// The convert byte to hex byte.
        /// </summary>
        /// <param name="data">
        /// The data.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string ConvertByteToHexByte(byte data)
        {
            string strResult = string.Format(@"{0:x2}", data);
            return strResult;
        }

        /// <summary>
        /// The convert integer to hex address.
        /// </summary>
        /// <param name="no">
        /// The no.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string ConvertIntegerToHexAddress(int no)
        {
            string strResult = string.Format(@"{0:x8}h:", no);

            return strResult;
        }
    }
}
