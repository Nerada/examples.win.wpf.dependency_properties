// -----------------------------------------------
//     Author: Ramon Bollen
//      File: DependencyProperties.DemoWindowSegmentedScrollBarViewModel.cs
// Created on: 20210811
// -----------------------------------------------

using System.Collections.Generic;
using System.Windows.Media;

namespace DependencyProperties;

public class DemoWindowSegmentedScrollBarViewModel
{
    public List<double> Segments => new() {50, 100, 150};

    public List<double> ViewerSegments => new() {2000, 4000, 6000};

    public List<Brush> SegmentColors => new()
    {
        new SolidColorBrush(Colors.Red),
        new SolidColorBrush(Colors.Green),
        new SolidColorBrush(Colors.Blue)
    };

    public List<Brush> RegionColors => new()
    {
        new SolidColorBrush(Colors.LightGreen) {Opacity = 0.3},
        new SolidColorBrush(Colors.LightBlue) {Opacity  = 0.3},
        new SolidColorBrush(Colors.Transparent),
        new SolidColorBrush(Colors.LightCoral) {Opacity = 0.3}
    };
}