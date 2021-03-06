﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;
using MagicMarbles.Extensions;
using MagicMarbles.Interfaces;
using MagicMarbles.Model;
using MagicMarbles.Utils;

namespace MagicMarbles.Helpers
{
    public class MarbleGameHandler : IGameHandler
    {
        #region Properties

        private CustomGrid Grid { get; set; }
        public int Highscore { get; set; }


        #endregion

        #region fields

        private readonly ButtonFactory _factory = new ButtonFactory();
        private readonly int _numberOfEnums = Enum.GetNames(typeof(Enums.Colors)).Length;
        private Button[,] _cells;
        private List<int> _cells2Swap;
        private ObservableCollection<Button> _buttons;

        #endregion

        public ObservableCollection<Button> BoardSetup(int row, int column, RelayCommand<object> selectButtonCommand)
        {
            Highscore = 0;
            _cells2Swap = new List<int>();
            Grid = new CustomGrid(row, column);
            var buttons = new ObservableCollection<Button>();

            for (var i = 0; i < row * column; i++)
            {
                CustomButton btn = _factory.CreateRandomButton(RandomNumberGenerator.Dice(0, _numberOfEnums),
                    selectButtonCommand, i);
                buttons.Add(btn);
            }
            buttons.Shuffle();
            _cells = buttons.To2DArray(Grid.Rows, Grid.Columns);
            RenewIndexes();
            return buttons;
        }

        public ObservableCollection<Button> MakeMove(ObservableCollection<Button> buttons, object commandparam)
        {
            _cells = buttons.To2DArray(Grid.Rows, Grid.Columns);
            _buttons = buttons;
            _cells2Swap.Clear();

            _cells2Swap.Add((int) commandparam);
            CheckNeighbors(GetIndexes((int) commandparam));
            _cells2Swap.Sort();
            CalculateHighscore();
            MoveVertically();
            MoveHorizontally();
            _cells2Swap.Clear();

            return _cells.Array2DToCollection();
        }

        public int CalculateHighscore()
        {
            if (_cells2Swap.Count > 1)
            {
                Highscore = Highscore + _cells2Swap.Count * _cells2Swap.Count;
            }
            return Highscore;
        }

        public string CheckWinLose()
        {
            _buttons = _cells.Array2DToCollection();
            if (_buttons.All(x => x.Visibility == Visibility.Hidden))
            {
                return "You Win!";
            }
            var moreMoves = false;
            var items = _buttons.ToList().FindAll(Predicate.ByVisibility(Visibility.Visible));
            foreach (var item in items)
            {
                CheckNeighbors(GetIndexes((int) item.CommandParameter));
                if (_cells2Swap.Count > 1)
                {
                    moreMoves = true;
                    break;
                }
            }
            if (moreMoves)
            {
                return string.Empty;
            }
            Highscore = Highscore - items.Count * 10;
            return "You Lose!";
        }

        private void MoveVertically()
        {
            if (_cells2Swap.Count <= 1) return;
            foreach (var cells in _cells2Swap)
            {
                for (var i = 0; i < Grid.Rows; i++)
                {
                    for (var a = 0; a < Grid.Columns; a++)
                    {
                        if (_cells[i, a].CommandParameter.Equals(cells))
                        {
                            _cells[i, a].Visibility = Visibility.Hidden;
                            _cells[i, a].Background = new SolidColorBrush(Colors.Black);
                            for (int j = i; j > 0; j--)
                            {
                                var swapelement = _cells[j, a];
                                _cells[j, a] = _cells[j - 1, a];
                                _cells[j - 1, a] = swapelement;
                            }
                        }
                    }
                }
                RenewIndexes();
            }
        }

        private void MoveHorizontally()
        {
            var swapCells = new List<int>();
            for (var k = 0; k < Grid.Columns; k++)
            {
                if (_cells[Grid.Rows - 1, k].Visibility == Visibility.Hidden)
                {
                    swapCells.Add(k);
                }
            }
            foreach (var unused in swapCells)
            {
                for (var i = 0; i < Grid.Columns - 1; i++)
                {
                    if (_cells[Grid.Rows - 1, i].Visibility == Visibility.Hidden)
                    {
                        for (var a = 0; a < Grid.Rows; a++)
                        {
                            var temp = _cells[a, i];
                            _cells[a, i] = _cells[a, i + 1];
                            _cells[a, i + 1] = temp;
                        }
                    }
                }
            }
            RenewIndexes();
        }

        private int[] GetIndexes(int commandparam)
        {
            var index = new int[2];
            for (var i = 0; i < Grid.Rows; i++)
            {
                for (var a = 0; a < Grid.Columns; a++)
                {
                    if (_cells[i, a].CommandParameter.Equals(commandparam))
                    {
                        index[0] = i;
                        index[1] = a;
                    }
                }
            }
            return index;
        }

        private void RenewIndexes()
        {
            var index = 0;
            for (var i = 0; i < Grid.Rows; i++)
            {
                for (var a = 0; a < Grid.Columns; a++)
                {
                    _cells[i, a].CommandParameter = index;
                    index++;
                }
            }
        }

        private void CheckNeighbors(IReadOnlyList<int> indexes)
        {
            var currentCell = _cells[indexes[0], indexes[1]];
            var right = (int) _cells[indexes[0], indexes[1]].CommandParameter + 1;
            var left = (int) _cells[indexes[0], indexes[1]].CommandParameter - 1;
            var top = (int) _cells[indexes[0], indexes[1]].CommandParameter - Grid.Columns;
            var bottom = (int) _cells[indexes[0], indexes[1]].CommandParameter + Grid.Columns;

            if (top >= 0)
            {
                if (currentCell.Background.ToString() == _buttons[top].Background.ToString())
                {
                    if (!_cells2Swap.Contains(top))
                    {
                        _cells2Swap.Add(top);
                        CheckNeighbors(GetIndexes(top));
                    }
                }
            }
            if (left >= 0)
            {
                if (!_cells2Swap.Contains(left))
                {
                    if (indexes[0] == GetIndexes(left)[0])
                    {
                        if (currentCell.Background.ToString() == _buttons[left].Background.ToString())
                        {
                            _cells2Swap.Add(left);
                            CheckNeighbors(GetIndexes(left));
                        }
                    }
                }
            }
            if (bottom < _buttons.Count)
            {
                if (!_cells2Swap.Contains(bottom))
                {
                    if (currentCell.Background.ToString() == _buttons[bottom].Background.ToString())
                    {
                        _cells2Swap.Add(bottom);
                        CheckNeighbors(GetIndexes(bottom));
                    }
                }
            }
            if (right < _buttons.Count)
            {
                if (!_cells2Swap.Contains(right))
                {
                    if (indexes[0] == GetIndexes(right)[0])
                    {
                        if (currentCell.Background.ToString() == _buttons[right].Background.ToString())
                        {
                            _cells2Swap.Add(right);
                            CheckNeighbors(GetIndexes(right));
                        }
                    }
                }
            }
        }
    }
}