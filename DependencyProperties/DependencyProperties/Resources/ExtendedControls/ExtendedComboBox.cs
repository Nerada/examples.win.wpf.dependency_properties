// -----------------------------------------------
//     Author: Ramon Bollen
//      File: DependencyProperties.ExtendedComboBox.cs
// Created on: 20220623
// -----------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DependencyProperties.Resources.ExtendedControls;

public class ExtendedComboBox : ComboBox
{
    /// <summary>
    ///     Foreground hover color extension
    /// </summary>
    public static readonly DependencyProperty HasForegroundHoverColorProperty =
        DependencyProperty.Register(nameof(HasForegroundHoverColor), typeof(bool), typeof(ExtendedComboBox), new PropertyMetadata(false));

    public static readonly DependencyProperty ForegroundHoverColorProperty =
        DependencyProperty.Register(nameof(ForegroundHoverColor), typeof(Brush), typeof(ExtendedComboBox), new PropertyMetadata(default(Brush), ForegroundHoverColorChangedCallback));

    /// <summary>
    ///     Background hover color extension
    /// </summary>
    public static readonly DependencyProperty HasBackgroundHoverColorProperty =
        DependencyProperty.Register(nameof(HasBackgroundHoverColor), typeof(bool), typeof(ExtendedComboBox), new PropertyMetadata(false));

    public static readonly DependencyProperty BackgroundHoverColorProperty =
        DependencyProperty.Register(nameof(BackgroundHoverColor), typeof(Brush), typeof(ExtendedComboBox), new PropertyMetadata(default(Brush), BackgroundHoverColorChangedCallback));


    public bool HasForegroundHoverColor => (bool)GetValue(HasForegroundHoverColorProperty);

    public bool HasBackgroundHoverColor => (bool)GetValue(HasBackgroundHoverColorProperty);

    public Brush ForegroundHoverColor
    {
        get => (Brush)GetValue(ForegroundHoverColorProperty);
        set => SetValue(ForegroundHoverColorProperty, value);
    }

    public Brush BackgroundHoverColor
    {
        get => (Brush)GetValue(BackgroundHoverColorProperty);
        set => SetValue(BackgroundHoverColorProperty, value);
    }

    private static void ForegroundHoverColorChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) =>
        (dependencyObject as ExtendedComboBox)?.SetValue(HasForegroundHoverColorProperty, true);

    private static void BackgroundHoverColorChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) =>
        (dependencyObject as ExtendedComboBox)?.SetValue(HasBackgroundHoverColorProperty, true);
}