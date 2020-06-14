using System.Windows.Controls;
using System.Windows;

namespace TabSwitcher
{
    public partial class TabSwitcherControl : UserControl
    {
        public event RoutedEventHandler btnPreviousClick;
        public event RoutedEventHandler btnNextClick;

        private bool _isHideBtnPrevious = false;
        private bool _isHidebtnNext = false;

        public bool IsHideBtnPrevious
        {
            get { return _isHideBtnPrevious; }
            set
            {
                _isHideBtnPrevious = value;
                SetButtons();
            }
        }

        public bool IsHideBtnNext
        {
            get { return _isHidebtnNext; }
            set
            {
                _isHidebtnNext = value;
                SetButtons();
            }
        }

        private void NextTruePreviousFalse()
        {
            btnNext.Visibility = Visibility.Hidden;
            btnPrevious.Visibility = Visibility.Visible;
            btnPrevious.Width = 229;
            btnNext.Width = 0;
            btnPrevious.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        private void PreviousTrueNextFalse()
        {
            btnPrevious.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Visible;
            btnNext.Width = 229;
            btnPrevious.Width = 0;
            btnNext.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        private void PreviousFalseNextFalse()
        {
            btnNext.Visibility = Visibility.Visible;
            btnPrevious.Visibility = Visibility.Visible;
            btnNext.Width = 115;
            btnPrevious.Width = 114;
        }

        private void PreviousTrueNextTrue()
        {
            btnPrevious.Visibility = Visibility.Hidden;
            btnNext.Visibility = Visibility.Hidden;
        }

        private void SetButtons()
        {
            if      ( _isHideBtnPrevious &&  _isHidebtnNext) PreviousTrueNextTrue();
            else if (!_isHideBtnPrevious && !_isHidebtnNext) PreviousFalseNextFalse();
            else if (!_isHideBtnPrevious &&  _isHidebtnNext) NextTruePreviousFalse();
            else if ( _isHideBtnPrevious && !_isHidebtnNext) PreviousTrueNextFalse();
        }

        public TabSwitcherControl()
        {
            InitializeComponent();
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            btnPreviousClick?.Invoke(sender, e);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            btnNextClick?.Invoke(sender, e);
        }
    }
}
