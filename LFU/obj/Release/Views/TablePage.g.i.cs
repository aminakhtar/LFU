﻿#pragma checksum "..\..\..\Views\TablePage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C900A1955B0DF6C09ACE5BB430E9B247"
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
    /// TablePage
    /// </summary>
    public partial class TablePage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPrevPage;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnNextPage;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbCurrentPage;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbLastPage;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbPageRowCount;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tblTotalRowCount;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmboFunctionField;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDropColumn;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddColumn;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSort;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnIndex;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnFilter;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tbSearchTerm;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock HitsResultLabel;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnImport;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Views\TablePage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAqc;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\Views\TablePage.xaml"
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
            System.Uri resourceLocater = new System.Uri("/LFU;component/views/tablepage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\TablePage.xaml"
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
            
            #line 18 "..\..\..\Views\TablePage.xaml"
            this.btnPrevPage.Click += new System.Windows.RoutedEventHandler(this.btnPrevPage_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnNextPage = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\Views\TablePage.xaml"
            this.btnNextPage.Click += new System.Windows.RoutedEventHandler(this.btnNextPage_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tbCurrentPage = ((System.Windows.Controls.TextBox)(target));
            
            #line 20 "..\..\..\Views\TablePage.xaml"
            this.tbCurrentPage.LostFocus += new System.Windows.RoutedEventHandler(this.tbCurrentPage_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.tbLastPage = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tbPageRowCount = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\..\Views\TablePage.xaml"
            this.tbPageRowCount.LostFocus += new System.Windows.RoutedEventHandler(this.tbPageRowCount_LostFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tblTotalRowCount = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.cmboFunctionField = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            this.btnDropColumn = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\Views\TablePage.xaml"
            this.btnDropColumn.Click += new System.Windows.RoutedEventHandler(this.btnDropColumn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnAddColumn = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\Views\TablePage.xaml"
            this.btnAddColumn.Click += new System.Windows.RoutedEventHandler(this.btnAddColumn_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnSort = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\Views\TablePage.xaml"
            this.btnSort.Click += new System.Windows.RoutedEventHandler(this.btnSort_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnIndex = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\Views\TablePage.xaml"
            this.btnIndex.Click += new System.Windows.RoutedEventHandler(this.btnIndex_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnFilter = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\..\Views\TablePage.xaml"
            this.btnFilter.Click += new System.Windows.RoutedEventHandler(this.btnFilter_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.tbSearchTerm = ((System.Windows.Controls.TextBox)(target));
            
            #line 47 "..\..\..\Views\TablePage.xaml"
            this.tbSearchTerm.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.tbSearchTerm_TextChanged);
            
            #line default
            #line hidden
            return;
            case 14:
            this.HitsResultLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 15:
            this.btnImport = ((System.Windows.Controls.Button)(target));
            
            #line 49 "..\..\..\Views\TablePage.xaml"
            this.btnImport.Click += new System.Windows.RoutedEventHandler(this.btnImport_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            this.btnAqc = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\Views\TablePage.xaml"
            this.btnAqc.Click += new System.Windows.RoutedEventHandler(this.btnAqc_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            this.dgData = ((System.Windows.Controls.DataGrid)(target));
            
            #line 63 "..\..\..\Views\TablePage.xaml"
            this.dgData.CellEditEnding += new System.EventHandler<System.Windows.Controls.DataGridCellEditEndingEventArgs>(this.MyDataGrid_CellEditEnding);
            
            #line default
            #line hidden
            
            #line 64 "..\..\..\Views\TablePage.xaml"
            this.dgData.ColumnReordered += new System.EventHandler<System.Windows.Controls.DataGridColumnEventArgs>(this.MyDataGrid_ColumnReordered);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

