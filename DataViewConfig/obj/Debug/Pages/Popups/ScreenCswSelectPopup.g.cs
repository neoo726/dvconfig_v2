﻿#pragma checksum "..\..\..\..\Pages\Popups\ScreenCswSelectPopup.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "58E30AEEA99F278A5FE52AFDB545B06E2EDC6584FDA9D05D4337CC6CB8A4B70A"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using DataViewConfig;
using DataViewConfig.Controls;
using DataViewConfig.Converters;
using DataViewConfig.Pages.Popups;
using DataViewConfig.ViewModels;
using DataView_Configuration;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace DataViewConfig.Pages.Popups {
    
    
    /// <summary>
    /// ScreenCswSelectPopup
    /// </summary>
    public partial class ScreenCswSelectPopup : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 2 "..\..\..\..\Pages\Popups\ScreenCswSelectPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DataViewConfig.Pages.Popups.ScreenCswSelectPopup ScreenCswSelectPopupWin;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\..\Pages\Popups\ScreenCswSelectPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView FileTreeView;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\..\..\Pages\Popups\ScreenCswSelectPopup.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView FileRightTreeView;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DataViewConfig;component/pages/popups/screencswselectpopup.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Pages\Popups\ScreenCswSelectPopup.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ScreenCswSelectPopupWin = ((DataViewConfig.Pages.Popups.ScreenCswSelectPopup)(target));
            return;
            case 2:
            this.FileTreeView = ((System.Windows.Controls.TreeView)(target));
            
            #line 53 "..\..\..\..\Pages\Popups\ScreenCswSelectPopup.xaml"
            this.FileTreeView.SelectedItemChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(this.FileTreeView_SelectedItemChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.FileRightTreeView = ((System.Windows.Controls.TreeView)(target));
            
            #line 83 "..\..\..\..\Pages\Popups\ScreenCswSelectPopup.xaml"
            this.FileRightTreeView.SelectedItemChanged += new System.Windows.RoutedPropertyChangedEventHandler<object>(this.FileRightTreeView_SelectedItemChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

