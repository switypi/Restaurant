﻿#pragma checksum "..\..\..\..\UserControls\MonthView.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E38736D1A3EDC0C1EE19A6F850E7EFDB5C8D43AE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using RestaurantDesk.UserControls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using Wpf.Ui;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Navigation;
using Wpf.Ui.Converters;
using Wpf.Ui.Markup;
using Wpf.Ui.ValidationRules;


namespace RestaurantDesk.UserControls {
    
    
    /// <summary>
    /// MonthView
    /// </summary>
    public partial class MonthView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 67 "..\..\..\..\UserControls\MonthView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label MonthYearLabel;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\UserControls\MonthView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MonthViewGrid;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/RestaurantDesk;component/usercontrols/monthview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\UserControls\MonthView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 35 "..\..\..\..\UserControls\MonthView.xaml"
            ((Wpf.Ui.Controls.Button)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.MonthGoPrev_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 49 "..\..\..\..\UserControls\MonthView.xaml"
            ((Wpf.Ui.Controls.Button)(target)).MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.MonthGoNext_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MonthYearLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.MonthViewGrid = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

