﻿// -----------------------------------------------
//     Author: Ramon Bollen
//      File: DependencyProperties.SegmentedScrollBarBehaviors.cs
// Created on: 20220623
// -----------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;

namespace DependencyProperties.Resources.ExtendedControls;

public class SegmentedScrollBarBehaviors
{
    private readonly SegmentedScrollBar _scrollBar;

    private bool _thumbBiggerThanSmallestSegment;

    private bool _thumbDragging;

    public SegmentedScrollBarBehaviors(SegmentedScrollBar scrollBar)
    {
        _scrollBar = scrollBar;

        _scrollBar.MouseEnter += (_, _) => NavigationCanExecuteChanged();
        _scrollBar.MouseLeave += (_, _) => NavigationCanExecuteChanged();
        _scrollBar.ValueChanged += (_, _) =>
        {
            NavigationCanExecuteChanged();
            SegmentNavigationCanExecuteChanged();
        };
        _scrollBar.ValueChanged += (_, _) => JumpOffSegmentBoundary();

        _scrollBar.PreviousSegmentCommand = new DelegateCommand(() => OnSegmentButtonClick(ButtonType.LeftSegmentButton),  () => CanExecutePreviousSegmentCommand);
        _scrollBar.NextSegmentCommand     = new DelegateCommand(() => OnSegmentButtonClick(ButtonType.RightSegmentButton), () => CanExecuteNextSegmentCommand);
    }

    private List<double> Boundaries => _scrollBar.SegmentBoundaries ?? new List<double>();

    private bool CanExecutePreviousSegmentCommand => !_thumbBiggerThanSmallestSegment && Boundaries.Count != 0 && _scrollBar.Value >= Boundaries[0];

    private bool CanExecuteNextSegmentCommand => !_thumbBiggerThanSmallestSegment && Boundaries.Count != 0 && _scrollBar.Value < Boundaries[^1];

    public void SegmentBoundariesChanged()
    {
        JumpOffSegmentBoundary();
        SegmentNavigationCanExecuteChanged();
    }

    public void OnApplyTemplate()
    {
        _scrollBar.Track.Thumb.GotMouseCapture  += (_, _) => _thumbDragging = true;
        _scrollBar.Track.Thumb.GotStylusCapture += (_, _) => _thumbDragging = true;
        _scrollBar.Track.Thumb.GotTouchCapture  += (_, _) => _thumbDragging = true;

        _scrollBar.Track.Thumb.LostMouseCapture += (_, _) =>
        {
            _thumbDragging = false;
            JumpOffSegmentBoundary();
        };
        _scrollBar.Track.Thumb.LostStylusCapture += (_, _) =>
        {
            _thumbDragging = false;
            JumpOffSegmentBoundary();
        };
        _scrollBar.Track.Thumb.LostTouchCapture += (_, _) =>
        {
            _thumbDragging = false;
            JumpOffSegmentBoundary();
        };

        _scrollBar.SizeChanged += (_, _) => CheckViewPortSize();
    }

    private void SegmentNavigationCanExecuteChanged()
    {
        _scrollBar.PreviousSegmentCommand.RaiseCanExecuteChanged();
        _scrollBar.NextSegmentCommand.RaiseCanExecuteChanged();
    }

    private void NavigationCanExecuteChanged()
    {
        _scrollBar.CanExecutePreviousCommand = _scrollBar.IsMouseOver && Math.Abs(_scrollBar.Value   - _scrollBar.Minimum) > double.Epsilon;
        _scrollBar.CanExecuteNextCommand     = _scrollBar.IsMouseOver && Math.Abs(_scrollBar.Maximum - _scrollBar.Value)   > double.Epsilon;
    }

    private void OnSegmentButtonClick(ButtonType buttonType)
    {
        // check if it comes from the right or left button, depending on that, switch to right or left segment.
        // If we go to the left direction - set scrollbar value to the end of the previous segment.
        // If the direction is to the right - to the beginning of the next segment.

        // Get current segment
        double? segmentValue = buttonType switch
        {
            ButtonType.LeftSegmentButton  => Boundaries.LastOrDefault(b => b <= _scrollBar.Value),
            ButtonType.RightSegmentButton => Boundaries.Find(b => b          > _scrollBar.Value),
            _                             => throw new ArgumentException($"{nameof(OnSegmentButtonClick)}: + Unsupported button type used.")
        };

        // Check if there is no right/left
        if (segmentValue is not { } segValue || segValue == 0) return;

        _scrollBar.Value = buttonType == ButtonType.LeftSegmentButton ? segValue - _scrollBar.ViewportSize : segValue;
    }

    /// <summary>
    ///     Check if ScrollBar thumb is at a segment boundary. Introduce jumping behaviour.
    /// </summary>
    private void JumpOffSegmentBoundary()
    {
        if (_thumbDragging || _thumbBiggerThanSmallestSegment) return;

        double? boundaryValue = Boundaries.Find(segment => segment > _scrollBar.Value && segment < _scrollBar.Value + _scrollBar.ViewportSize);

        if (boundaryValue is not { } boundary || boundary == 0) return;

        double halfThumbValue = _scrollBar.Value + _scrollBar.ViewportSize / 2;

        // Jump to the left or right of a segment boundary
        _scrollBar.Value = halfThumbValue < boundary ? boundary - _scrollBar.ViewportSize : boundary;
    }

    private void CheckViewPortSize()
    {
        List<double> checkBoundaries = new(Boundaries);
        checkBoundaries.Insert(0, 0);
        checkBoundaries.Add(_scrollBar.Maximum + _scrollBar.Track.ViewportSize);

        if (_scrollBar.ViewportSize < 1) return;

        double smallestSegment = SmallestDifference(checkBoundaries);

        if (_scrollBar is {ScrollViewer: { } scrollViewer, ViewportSize: > 1} &&
            Math.Abs(scrollViewer.ActualWidth - _scrollBar.ViewportSize) > double.Epsilon)
        {
            double scrollViewerDiff = _scrollBar.ViewportSize - scrollViewer.ActualWidth;
            _scrollBar.ViewportSize  =  scrollViewer.ActualWidth;
            _scrollBar.Track.Maximum += scrollViewerDiff;

            _thumbBiggerThanSmallestSegment = _scrollBar.ViewportSize > smallestSegment;
            SegmentNavigationCanExecuteChanged();
        }
    }

    private static double SmallestDifference(IReadOnlyList<double> source)
    {
        double difference = double.MaxValue;
        for (int i = 1; i < source.Count; i++) { difference = Math.Min(difference, source[i] - source[i - 1]); }

        return difference;
    }

    private enum ButtonType
    {
        LeftSegmentButton,
        RightSegmentButton
    }
}