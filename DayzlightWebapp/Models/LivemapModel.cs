using DayzlightCommon.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DayzlightWebapp.Models
{
    public class LivemapModel
    {
        public ServerRestartEntity ServerLastRestartInfo { set; get; }
        public ServerRestartEntity ServerCurRestartInfo { set; get; }
        public ServerRestartEntity ServerNextRestartInfo { set; get; }
        public TimepointEntity[] Timepoints { set; get; }
        public bool ExpandMenu { set; get; }
    }
}