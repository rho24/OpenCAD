using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Controls;
using AvalonDock.Layout;
using OpenCAD.Core.Graphics;
using OpenCAD.Core.Maths;
using OpenCAD.Core.Modeling;
using OpenCAD.Core.Modeling.Datums;
using OpenCAD.GUI.Buffers;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;
using System.Collections.Generic;
using System.Reactive;

namespace OpenCAD.GUI
{
    public partial class CADDocument
    {
        private readonly IModel _model;

        public CADDocument(IModel model)
        {
            _model = model;
            InitializeComponent();
            Content = new ModelControl(_model);
            Title = string.Format("Part: {0}", model.Name);
        }
    }
}