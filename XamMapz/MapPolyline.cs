﻿//
// MapRoute.cs
//
// Author:
//    Gabor Nemeth (gabor.nemeth.dev@gmail.com)
//
//    Copyright (C) 2015, Gabor Nemeth
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Linq;

namespace XamMapz
{
    /// <summary>
    /// Polyline
    /// </summary>
    public class MapPolyline : BindableObject
    {
        public IList<Position> Positions { get; private set; }

        #region Color bindable property

        public static readonly BindableProperty ColorProperty = BindableProperty.Create<MapPolyline, Color>(route => route.Color, Color.Default);

        public Color Color
        {
            get
            {
                return (Color)GetValue(ColorProperty);
            }
            set
            {
                SetValue(ColorProperty, value);
            }
        }

        #endregion

        public double Width { get; set; }

        public event EventHandler<MapRoutePositionChangeEventArgs> PositionChanged;

        public MapPolyline()
        {
            var positions = new ObservableCollection<Position>();
            positions.CollectionChanged += Positions_CollectionChanged;
            Positions = positions;
        }

        private void Positions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (PositionChanged != null)
                    PositionChanged(this, new MapRoutePositionChangeEventArgs(NotifyCollectionChangedAction.Add, e.NewItems.Cast<Position>().ToList()));
            }
        }
    }
}