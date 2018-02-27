using UICore.Utility.Element;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UICore.Controls.Metro
{
    public class MetroMenuHorTabControl : TabControl
    {

        public static readonly DependencyProperty TabPanelHorizonAlignmentProperty = 
            ElementBase.Property<MetroMenuHorTabControl, HorizontalAlignment>(nameof(TabPanelHorizonAlignmentProperty), HorizontalAlignment.Center);

        public static readonly DependencyProperty OffsetProperty = 
            ElementBase.Property<MetroMenuHorTabControl, Thickness>(nameof(OffsetProperty), new Thickness(0));

        public static readonly DependencyProperty IconModeProperty = 
            ElementBase.Property<MetroMenuHorTabControl, bool>(nameof(IconModeProperty), false);

        public static RoutedUICommand IconModeClickCommand = ElementBase.Command<MetroMenuHorTabControl>(nameof(IconModeClickCommand));

        public HorizontalAlignment TabPanelHorizonAlignment
        {
            get { return (HorizontalAlignment)GetValue(TabPanelHorizonAlignmentProperty); }
            set { SetValue(TabPanelHorizonAlignmentProperty, value); }
        }

        public Thickness Offset
        {
            get { return (Thickness)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        public bool IconMode
        {
            get { return (bool)GetValue(IconModeProperty); }
            set { SetValue(IconModeProperty, value); GoToState(); }
        }

        void GoToState()
        {
            ElementBase.GoToState(this, IconMode ? "EnterIconMode" : "ExitIconMode");
        }

        void SelectionState()
        {
            if (IconMode)
            {
                ElementBase.GoToState(this, "SelectionStartIconMode");
                ElementBase.GoToState(this, "SelectionEndIconMode");
            }
            else
            {
                ElementBase.GoToState(this, "SelectionStart");
                ElementBase.GoToState(this, "SelectionEnd");
            }
        }

        public MetroMenuHorTabControl()
        {
            Loaded += delegate { GoToState(); ElementBase.GoToState(this, IconMode ? "SelectionLoadedIconMode" : "SelectionLoaded"); };
            SelectionChanged += delegate (object sender, SelectionChangedEventArgs e) { if (e.Source is MetroMenuHorTabControl) { SelectionState(); } };
            CommandBindings.Add(new CommandBinding(IconModeClickCommand, delegate { IconMode = !IconMode; GoToState(); }));
            
            Utility.Refresh(this);
        }

        static MetroMenuHorTabControl()
        {
            ElementBase.DefaultStyle<MetroMenuHorTabControl>(DefaultStyleKeyProperty);
        }
    }
}
