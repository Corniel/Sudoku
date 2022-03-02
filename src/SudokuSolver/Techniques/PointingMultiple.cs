﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver.Techniques;

public abstract class PointingMultiple : Technique
{
    protected abstract int Size{ get; }

    public Cells Reduce(Cells cells, Regions regions)
    {
        foreach (var region in regions.Where(r => r.Type == RegionType.Block))
        {
            foreach(var value in Values.Singles)
            {
                cells = CheckCells(cells, value, region, regions);
            }
        }
        return cells;
    }

    private Cells CheckCells(Cells cells, Values value, Region region, Regions regions)
    {
        var pointing = new List<Location>(Size);

        foreach(var cell in cells.Region(region))
        {
            if (cell.Values == value)
            {
                return cells;
            }
            else if ((cell.Values & value) != default)
            {
                if (pointing.Count < Size)
                {
                    pointing.Add(cell.Location);
                }
                else return cells;
            }
        }
        if (pointing.Count == Size)
        {
            foreach(var other in regions.Where(r => pointing.All(p => r.Contains(p))))
            {
                foreach(var loc in other.Where(l => !pointing.Contains(l)))
                {
                    cells = cells.Not(loc, value);
                }
            }
        }
        return cells;

    }
}
