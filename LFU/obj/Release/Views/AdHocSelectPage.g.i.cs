﻿#pragma checksum "..\..\..\Views\AdHocSelectPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "818D8C0E9D853C6BC7E805681DADF29C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace LFU.Views {
    
    
    /// <summary>
    /// AdHocSelectPage
    /// </summary>
    public partial class AdHocSelectPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\Views\AdHocSelectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrevPage;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Views\AdHocSelectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNextPage;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Views\AdHocSelectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbCurrentPage;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Views\AdHocSelectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbLastPage;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Views\AdHocSelectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbPageRowCount;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Views\AdHocSelectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblTotalRowCount;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\Views\AdHocSelectPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgData;
        
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
            System.Uri resourceLocater = new System.Uri("/LFU;component/views/adhocselectpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\AdHocSelectPage.xaml"
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
            this.btnPrevPage = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\Views\AdHocSelectPage.xaml"
            this.btnPrevPage.Click += new System.Windows.RoutedEventHandler(this.btnPrevPage_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnNextPage = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\Views\AdHocSelectPage.xaml"
            this.btnNextPage.Click += new System.Windows.RoutedEventHandler(this.btnNextPage_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbCurrentPage = ((System.Windows.Controls.TextBox)(target));
            
            #line 20 "..\..\..\Views\AdHocSelectPage.xaml"
            this.tbCurrentPage.LostFocus += new System.Windows.RoutedEventHandler(this.tbCurrentPage_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tbLastPage = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tbPageRowCount = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\..\Views\AdHocSelectPage.xaml"
            this.tbPageRowCount.LostFocus += new System.Windows.RoutedEventHandler(this.tbPageRowCount_LostFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tblTotalRowCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.dgData = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
