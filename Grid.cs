using System;
using System.Windows.Forms;

namespace AutoTest
{
    class Grid
    {
        //Utility function to copy a row with it's values because the built in clone just copy's properties
        public static DataGridViewRow CloneWithValues(DataGridViewRow row)
        {
            DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
            for (Int32 index = 0; index < row.Cells.Count; index++)
            {
                clonedRow.Cells[index].Value = row.Cells[index].Value;
            }
            return clonedRow;
        }

        //Add consecutive rows
        //cprow is the row to copy from
        //end is the ending serial number
        public static void addCons(DataGridView dgv, String end, int cprow)
        {
            if (dgv.Rows[cprow].Cells[1].Value != null)
            {
                String start = dgv.Rows[cprow].Cells[1].Value.ToString();
                String ltrdt = start.Substring(0, 7);
                start = start.Substring(7);
                end = end.Substring(7);
                for (int i = 1; i <= int.Parse(end) - int.Parse(start); i++)
                {
                    DataGridViewRow clone = Grid.CloneWithValues(dgv.Rows[cprow]);
                    clone.Cells[1].Value = ltrdt + (int.Parse(start) + i).ToString().PadLeft(4, '0');
                    dgv.Rows.Add(clone);
                }
            }
        }

        //Check if a part/row is tested
        //Returns true if a part is done(green) or currently being tested(orange)
        public static bool tested(DataGridView dgv, int row)
        {
            if (dgv.Rows[row].Cells[0].Style.BackColor == System.Drawing.Color.Red)
            {
                return false;
            }
            return true;
        }

        //returns row containing the specified serial
        public static int findSerial(DataGridView dgv, String serial)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[1].Value != null && row.Cells[1].Value.Equals(serial))
                {
                    return row.Index;
                }
            }
            return -1;
        }

        //Checks if all rows/parts have been tested in the dgv
        public static bool done(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.Cells[0].Style.BackColor != System.Drawing.Color.Green && row.Index != dgv.RowCount)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
