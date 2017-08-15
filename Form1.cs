using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace GetAllCombinations
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            var numbers = new List<int>
            {
                Convert.ToInt32(textBox1.Text),
                Convert.ToInt32(textBox2.Text),
                Convert.ToInt32(textBox3.Text),
                Convert.ToInt32(textBox4.Text)
            };

            var result = GetPermutations(numbers, numbers.Count);
            var combinations = result as IList<IEnumerable<int>> ?? result.ToList();
            listBox1.Items.Add("Total items " + combinations.Count().ToString(CultureInfo.InvariantCulture) + Environment.NewLine);
            foreach (var combination in combinations)
            {
                var resultData = combination.Aggregate(0, (current, number) => (current*10) + number);
                listBox1.Items.Add(resultData.ToString());
            }
        }


        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
