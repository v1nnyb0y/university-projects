using System.Collections.Generic;

namespace Lab._2.Paint.Interfaces.UIInt
{
    public interface IWorkspace
    {
        int[] Indexes { get; set; }

        List<string> Names { get; set; }
    }
}