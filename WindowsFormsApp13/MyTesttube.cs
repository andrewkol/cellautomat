using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp13
{
    class MyTesttube
    {
        private int countOfCellsInTube, countOfOrganismsToPlace;
        private List<MyCell> AllCells;
        private TableLayoutPanel myPanelTube;
        private List<Panel> AllPanelsInTube;
        private List<MyCell> CellTodelete;
        private List<MyCell> CellToAdd;


        public MyTesttube(int count, int org, TableLayoutPanel pan)
        {
            countOfCellsInTube = count;
            countOfOrganismsToPlace = org;
            AllCells = new List<MyCell>() { };
            AllPanelsInTube = new List<Panel>() { };
            myPanelTube = pan;
            CellTodelete = new List<MyCell>() { };
            CellToAdd = new List<MyCell>() { };
        }
        public int _RealCount { get { return AllCells.Count; }}
        public void SetArr()
        {
            while(countOfOrganismsToPlace > 0)
            {
                for (int i = 0; i < myPanelTube.ColumnCount; i++)
                {
                    for (int j = 0; j < myPanelTube.RowCount; j++)
                    {
                        if(CheckTube(i, j) && countOfOrganismsToPlace > 0)
                        {
                            switch (Rand.GetRandom(1, 15))
                            {
                                case 1:
                                    {
                                        AllCells.Add(new MyAmoeba(10, 0, (int)myPanelTube.ColumnStyles[0].Width - 30, (int)myPanelTube.RowStyles[0].Height - 10, 3, i, j));
                                        AllPanelsInTube.Add(new Panel());
                                        AllPanelsInTube[AllPanelsInTube.Count - 1].Width = (int)myPanelTube.ColumnStyles[0].Width;
                                        AllPanelsInTube[AllPanelsInTube.Count - 1].Height = (int)myPanelTube.RowStyles[0].Height;
                                        myPanelTube.Controls.Add(AllPanelsInTube[AllPanelsInTube.Count - 1], i, j);
                                        AllCells[AllCells.Count - 1].Draw(AllPanelsInTube[AllPanelsInTube.Count - 1]);
                                        countOfOrganismsToPlace -= 1;
                                        break;
                                    }
                                case 2:
                                    {
                                        AllCells.Add(new MyInfusoria(5, 2, (int)myPanelTube.ColumnStyles[0].Width - 10, (int)myPanelTube.RowStyles[0].Height - 5, 2, i, j));
                                        AllPanelsInTube.Add(new Panel());
                                        AllPanelsInTube[AllPanelsInTube.Count - 1].Width = (int)myPanelTube.ColumnStyles[0].Width;
                                        AllPanelsInTube[AllPanelsInTube.Count - 1].Height = (int)myPanelTube.RowStyles[0].Height;
                                        myPanelTube.Controls.Add(AllPanelsInTube[AllPanelsInTube.Count - 1], i, j);
                                        AllCells[AllCells.Count - 1].Draw(AllPanelsInTube[AllPanelsInTube.Count - 1]);
                                        countOfOrganismsToPlace -= 1;
                                        break;
                                    }
                                case 3:
                                    {
                                        AllCells.Add(new MyBacteria(2, 2, (int)myPanelTube.ColumnStyles[0].Width - 10, (int)myPanelTube.RowStyles[0].Height / 2, 1, i, j));
                                        AllPanelsInTube.Add(new Panel());
                                        AllPanelsInTube[AllPanelsInTube.Count - 1].Width = (int)myPanelTube.ColumnStyles[0].Width;
                                        AllPanelsInTube[AllPanelsInTube.Count - 1].Height = (int)myPanelTube.RowStyles[0].Height;
                                        myPanelTube.Controls.Add(AllPanelsInTube[AllPanelsInTube.Count - 1], i, j);
                                        AllCells[AllCells.Count - 1].Draw(AllPanelsInTube[AllPanelsInTube.Count - 1]);
                                        countOfOrganismsToPlace-=1;
                                        break;
                                    }
                                default:
                                    break;
                            }
                        }
                    }
                }
            }
        }
        public void DrawG()
        {
            for (int i = 0; i < AllCells.Count; i++)
            {
                AllCells[i].Draw(AllPanelsInTube[i]);
            }
        }
        public bool CheckTube(int i, int j)
        {
            if (i < 0 || j < 0 || i >= myPanelTube.ColumnCount || j >= myPanelTube.RowCount)
                return false;
            Control c = myPanelTube.GetControlFromPosition(i, j);
            if (c == null)
                return true;
            else
                return false;
        }
        enum Step : int { Move = 1, Death = 2, Birth = 3 }
        public void NextStep()
        {
            foreach (var item in AllCells)
            {
                switch (Rand.GetRandom(1, 4))
                {
                    case (int)Step.Move:
                        {
                            for(int i = 0; i < item._movement; i++)
                            {
                                Move(item);
                            }
                            break;
                        }
                    case (int)Step.Death:
                        {
                            if (item._chanceOfDeath * Rand.GetRandom(1, 11) > 2.0)
                            {
                                CellTodelete.Add(item);
                            }
                            else
                                goto case 1;
                            break;
                        }
                    case (int)Step.Birth:
                        {
                            if (item._chanceOfBirth * Rand.GetRandom(1, 11) > 2.0)
                            {
                                CellToAdd.Add(item);
                            }
                            else
                                goto case 1;
                            break;
                        }
                }
            }
            if(CellTodelete.Count > 0)
            {
                for(int i = 0; i < CellTodelete.Count; i++)
                {
                    for (int j = 0; j < AllCells.Count; j++)
                    {
                        if(CellTodelete[i] == AllCells[j])
                        {
                            int currenti = AllCells[j]._IndexI;
                            int currentj = AllCells[j]._IndexJ;
                            myPanelTube.Controls.Remove(myPanelTube.GetControlFromPosition(currenti, currentj));
                            AllPanelsInTube.RemoveAt(j);
                            AllCells.RemoveAt(j);
                        }
                    }
                }
                CellTodelete.Clear();
            }
            if(CellToAdd.Count > 0)
            {
                foreach(var item in CellToAdd)
                {
                    Born(item);
                }
                CellToAdd.Clear();
            }    
            myPanelTube.Refresh();
            DrawG();
        }
        enum Sides :int {Left = 1, Right, Down, Up, Up_Left, Up_Right, Down_Left, Down_Right }
        public void Move(MyCell cell)
        {
            int currenti = cell._IndexI;
            int currentj = cell._IndexJ;
            Control c = myPanelTube.GetControlFromPosition(currenti, currentj);
            Control b = c;
            switch (cell.Move())
            {
                case (int)Sides.Left:
                    {
                        if(CheckTube(currenti, currentj - 1))
                        {
                            myPanelTube.Controls.Remove(b);
                            myPanelTube.Controls.Add(b, currenti, currentj - 1);
                            cell._IndexJ--;
                        }
                        break;
                    }
                case (int)Sides.Right:
                    {
                        if (CheckTube(currenti, currentj + 1))
                        {
                            myPanelTube.Controls.Remove(b);
                            myPanelTube.Controls.Add(b, currenti, currentj + 1);
                            cell._IndexJ++;
                        }
                        break;
                    }
                case (int)Sides.Down:
                    {
                        if (CheckTube(currenti - 1, currentj))
                        {
                            myPanelTube.Controls.Remove(b);
                            myPanelTube.Controls.Add(b, currenti - 1, currentj);
                            cell._IndexI--;
                        }
                        break;
                    }
                case (int)Sides.Up:
                    {
                        if (CheckTube(currenti + 1, currentj))
                        {
                            myPanelTube.Controls.Remove(b);
                            myPanelTube.Controls.Add(b, currenti + 1, currentj);
                            cell._IndexI++;
                        }
                        break;
                    }
                case (int)Sides.Up_Left:
                    {
                        if (CheckTube(currenti + 1, currentj - 1))
                        {
                            myPanelTube.Controls.Remove(b);
                            myPanelTube.Controls.Add(b, currenti + 1, currentj - 1);
                            cell._IndexI++;
                            cell._IndexJ--;
                        }
                        break;
                    }
                case (int)Sides.Up_Right:
                    {
                        if (CheckTube(currenti + 1, currentj + 1))
                        {
                            myPanelTube.Controls.Remove(b);
                            myPanelTube.Controls.Add(b, currenti + 1, currentj + 1);
                            cell._IndexI++;
                            cell._IndexJ++;
                        }
                        break;
                    }
                case (int)Sides.Down_Left:
                    {
                        if (CheckTube(currenti - 1, currentj - 1))
                        {
                            myPanelTube.Controls.Remove(b);
                            myPanelTube.Controls.Add(b, currenti - 1, currentj - 1);
                            cell._IndexI--;
                            cell._IndexJ--;
                        }
                        break;
                    }
                case (int)Sides.Down_Right:
                    {
                        if (CheckTube(currenti - 1, currentj + 1))
                        {
                            myPanelTube.Controls.Remove(b);
                            myPanelTube.Controls.Add(b, currenti - 1, currentj + 1);
                            cell._IndexI--;
                            cell._IndexJ++;
                        }
                        break;
                    }
            }
        }
        public void Born(MyCell cell)
        {
            int currenti = cell._IndexI;
            int currentj = cell._IndexJ;
            // {bacteria = 0.5, infusoria = 0.3, amoeba = 0.3}
            if (CheckTube(currenti, currentj - 1))
            {
                switch(cell._chanceOfBirth)
                {
                    case 0.3:
                        {
                            first(currenti, currentj - 1);
                            break;
                        }
                    case 0.5:
                        {
                            third(currenti, currentj - 1);
                            break;
                        }
                    case 0.6:
                        {
                            second(currenti, currentj - 1);
                            break;
                        }
                }
            }
            else if (CheckTube(currenti, currentj + 1))
            {
                switch (cell._chanceOfBirth)
                {
                    case 0.3:
                        {
                            first(currenti, currentj + 1);
                            break;
                        }
                    case 0.5:
                        {
                            third(currenti, currentj + 1);
                            break;
                        }
                    case 0.6:
                        {
                            second(currenti, currentj + 1);
                            break;
                        }
                }
            }
            else if (CheckTube(currenti - 1, currentj))
            {
                switch (cell._chanceOfBirth)
                {
                    case 0.3:
                        {
                            first(currenti - 1, currentj );
                            break;
                        }
                    case 0.5:
                        {
                            third(currenti - 1, currentj);
                            break;
                        }
                    case 0.6:
                        {
                            second(currenti - 1, currentj);
                            break;
                        }
                }
            }
            else if (CheckTube(currenti + 1, currentj))
            {
                switch (cell._chanceOfBirth)
                {
                    case 0.3:
                        {
                            first(currenti + 1, currentj);
                            break;
                        }
                    case 0.5:
                        {
                            third(currenti + 1, currentj);
                            break;
                        }
                    case 0.6:
                        {
                            second(currenti + 1, currentj);
                            break;
                        }
                }
            }
            myPanelTube.Refresh();
            DrawG();
        }
        private void first(int i, int j)
        {
            AllCells.Add(new MyAmoeba(10, 0, (int)myPanelTube.ColumnStyles[0].Width - 30, (int)myPanelTube.RowStyles[0].Height - 10, 3, i, j));
            AllPanelsInTube.Add(new Panel());
            AllPanelsInTube[AllPanelsInTube.Count - 1].Width = (int)myPanelTube.ColumnStyles[0].Width;
            AllPanelsInTube[AllPanelsInTube.Count - 1].Height = (int)myPanelTube.RowStyles[0].Height;
            myPanelTube.Controls.Add(AllPanelsInTube[AllPanelsInTube.Count - 1], i, j);
            AllCells[AllCells.Count - 1].Draw(AllPanelsInTube[AllPanelsInTube.Count - 1]);
        }
        private void second(int i, int j)
        {
            AllCells.Add(new MyInfusoria(5, 2, (int)myPanelTube.ColumnStyles[0].Width - 10, (int)myPanelTube.RowStyles[0].Height - 5, 2, i, j));
            AllPanelsInTube.Add(new Panel());
            AllPanelsInTube[AllPanelsInTube.Count - 1].Width = (int)myPanelTube.ColumnStyles[0].Width;
            AllPanelsInTube[AllPanelsInTube.Count - 1].Height = (int)myPanelTube.RowStyles[0].Height;
            myPanelTube.Controls.Add(AllPanelsInTube[AllPanelsInTube.Count - 1], i, j);
            AllCells[AllCells.Count - 1].Draw(AllPanelsInTube[AllPanelsInTube.Count - 1]);
        }
        private void third(int i, int j)
        {
            AllCells.Add(new MyBacteria(2, 2, (int)myPanelTube.ColumnStyles[0].Width - 10, (int)myPanelTube.RowStyles[0].Height / 2, 1, i, j));
            AllPanelsInTube.Add(new Panel());
            AllPanelsInTube[AllPanelsInTube.Count - 1].Width = (int)myPanelTube.ColumnStyles[0].Width;
            AllPanelsInTube[AllPanelsInTube.Count - 1].Height = (int)myPanelTube.RowStyles[0].Height;
            myPanelTube.Controls.Add(AllPanelsInTube[AllPanelsInTube.Count - 1], i, j);
            AllCells[AllCells.Count - 1].Draw(AllPanelsInTube[AllPanelsInTube.Count - 1]);
        }
    }
}
