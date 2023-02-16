// -----------------------------------------------
//     Author: Ramon Bollen
//      File: DependencyProperties.ExtendedButton.cs
// Created on: 20220623
// -----------------------------------------------

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DependencyProperties.Resources.ExtendedControls;

/// <summary>
///     Based on: https://stackoverflow.com/questions/815797/add-dependency-property-to-control
/// </summary>
public sealed class ExtendedButton : Button
{
    /// <summary>
    ///     Foreground color extension
    /// </summary>
    public new static readonly DependencyProperty ForegroundProperty =
        DependencyProperty.Register(nameof(Foreground), typeof(Brush), typeof(ExtendedButton), new PropertyMetadata(default(Brush), ForegroundColorChangedCallback));

    public static readonly DependencyProperty HasForegroundColorProperty =
        DependencyProperty.Register(nameof(HasForegroundColor), typeof(bool), typeof(ExtendedButton), new PropertyMetadata(false));

    /// <summary>
    ///     Background color extension
    /// </summary>
    public new static readonly DependencyProperty BackgroundProperty =
        DependencyProperty.Register(nameof(Background), typeof(Brush), typeof(ExtendedButton), new PropertyMetadata(default(Brush), BackgroundColorChangedCallback));

    public static readonly DependencyProperty HasBackgroundColorProperty =
        DependencyProperty.Register(nameof(HasBackgroundColor), typeof(bool), typeof(ExtendedButton), new PropertyMetadata(false));

    /// <summary>
    ///     Background hover color extension
    /// </summary>
    public static readonly DependencyProperty HasBackgroundHoverColorProperty =
        DependencyProperty.Register(nameof(HasBackgroundHoverColor), typeof(bool), typeof(ExtendedButton), new PropertyMetadata(false));

    public static readonly DependencyProperty BackgroundHoverColorProperty =
        DependencyProperty.Register(nameof(BackgroundHoverColor), typeof(Brush), typeof(ExtendedButton), new PropertyMetadata(default(Brush), BackgroundHoverColorChangedCallback));


    /// <summary>
    ///     Foreground hover color extension
    /// </summary>
    public static readonly DependencyProperty HasForegroundHoverColorProperty =
        DependencyProperty.Register(nameof(HasForegroundHoverColor), typeof(bool), typeof(ExtendedButton), new PropertyMetadata(false));

    public static readonly DependencyProperty ForegroundHoverColorProperty =
        DependencyProperty.Register(nameof(ForegroundHoverColor), typeof(Brush), typeof(ExtendedButton), new PropertyMetadata(default(Brush), ForegroundHoverColorChangedCallback));

    /// <summary>
    ///     Image extension
    /// </summary>
    public static readonly DependencyProperty HasImageProperty =
        DependencyProperty.Register(nameof(HasImage), typeof(bool), typeof(ExtendedButton), new PropertyMetadata(false));

    public static readonly DependencyProperty ImageProperty =
        DependencyProperty.Register(nameof(Image), typeof(DrawingImage), typeof(ExtendedButton), new PropertyMetadata(default(DrawingImage), ImageChangedCallback));

    public static readonly DependencyProperty ImageMarginProperty =
        DependencyProperty.Register(nameof(ImageMargin), typeof(Thickness), typeof(ExtendedButton), new PropertyMetadata(default(Thickness), ImageChangedCallback));

    /// <summary>
    ///     Image hover extension
    /// </summary>
    public static readonly DependencyProperty HasImageHoverProperty =
        DependencyProperty.Register(nameof(HasImageHover), typeof(bool), typeof(ExtendedButton), new PropertyMetadata(false));

    public static readonly DependencyProperty ImageHoverProperty =
        DependencyProperty.Register(nameof(ImageHover), typeof(DrawingImage), typeof(ExtendedButton), new PropertyMetadata(default(DrawingImage), ImageHoverChangedCallback));

    /// <summary>
    ///     Additional content extension
    /// </summary>
    public static readonly DependencyProperty Content2Property =
        DependencyProperty.Register(nameof(Content2), typeof(string), typeof(ExtendedButton), new PropertyMetadata(default(string), Content2ChangedCallBack));


    public bool HasForegroundColor => (bool)GetValue(HasForegroundColorProperty);

    public bool HasBackgroundColor => (bool)GetValue(HasBackgroundColorProperty);

    public bool HasBackgroundHoverColor => (bool)GetValue(HasBackgroundHoverColorProperty);

    public bool HasForegroundHoverColor => (bool)GetValue(HasForegroundHoverColorProperty);

    public bool HasImage => (bool)GetValue(HasImageProperty);

    public bool HasImageHover => (bool)GetValue(HasImageHoverProperty);

    public new Brush Foreground
    {
        get => (Brush)GetValue(ForegroundProperty);
        set => SetValue(ForegroundProperty, value);
    }

    public new Brush Background
    {
        get => (Brush)GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    public Brush BackgroundHoverColor
    {
        get => (Brush)GetValue(BackgroundHoverColorProperty);
        set => SetValue(BackgroundHoverColorProperty, value);
    }

    public Brush ForegroundHoverColor
    {
        get => (Brush)GetValue(ForegroundHoverColorProperty);
        set => SetValue(ForegroundHoverColorProperty, value);
    }

    public DrawingImage Image
    {
        get => (DrawingImage)GetValue(ImageProperty);
        set => SetValue(ImageProperty, value);
    }

    public Thickness ImageMargin
    {
        get => (Thickness)GetValue(ImageMarginProperty);
        set => SetValue(ImageMarginProperty, value);
    }

    public DrawingImage ImageHover
    {
        get => (DrawingImage)GetValue(ImageHoverProperty);
        set => SetValue(ImageHoverProperty, value);
    }

    public string Content2
    {
        set => SetValue(Content2Property, value);
    }

    private static void ForegroundColorChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) =>
        (dependencyObject as ExtendedButton)?.SetValue(HasForegroundColorProperty, true);

    private static void BackgroundColorChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) =>
        (dependencyObject as ExtendedButton)?.SetValue(HasBackgroundColorProperty, true);

    private static void BackgroundHoverColorChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) =>
        (dependencyObject as ExtendedButton)?.SetValue(HasBackgroundHoverColorProperty, true);

    private static void ForegroundHoverColorChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) =>
        (dependencyObject as ExtendedButton)?.SetValue(HasForegroundHoverColorProperty, true);

    private static void ImageChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) =>
        (dependencyObject as ExtendedButton)?.SetValue(HasImageProperty, true);

    private static void ImageHoverChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args) =>
        (dependencyObject as ExtendedButton)?.SetValue(HasImageHoverProperty, true);

    private static void Content2ChangedCallBack(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
    {
        if (dependencyObject as ExtendedButton is not { } extendedButton) return;

        extendedButton.Content += (string)args.NewValue;
    }
}