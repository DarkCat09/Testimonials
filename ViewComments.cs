using System.Collections.Generic;
using System.Windows.Forms;

namespace Booking3
{
    public partial class ViewComments : Form
    {
        public ViewComments(string _hotelId)
        {
            InitializeComponent();
            List<string> allComments = SQLClass.Select(
                $"SELECT User,Rate,Comment FROM rating WHERE Hotel_ID = {_hotelId}"
            );
            for (int i = 0; i < allComments.Count; i+=3)
            {
                textBox1.Text +=
                    $"User {allComments[i+0]}: Rated {allComments[i+1]}, Text|{allComments[i+2]}" +
                    $"{System.Environment.NewLine}{System.Environment.NewLine}";
            }
        }
    }
}
