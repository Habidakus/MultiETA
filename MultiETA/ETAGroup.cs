namespace MultiETA
{

    public class ETAGroup
    {

        private TextBox text_box = new();
        private Label run_state_label = new Label();
        private Label last_update_label = new Label();
        private Label goal_label = new Label();
        private Label rate_label = new Label();
        private Label eta_label = new Label();
        private Label estemated_current_value_label = new Label();
        private GroupBox group_box { get; }
        private float goal = 0f;
        private AdaptiveETA? adaptive_eta = null;
        private enum RunState { NeedsName, NeedsGoal, NeedsStartValue, Running, Removed }
        private RunState run_state = RunState.NeedsName;

        const int GROUP_BOX_START_Y = 40;
        const int GROUP_BOX_HEIGHT = 57;
        const int GROUP_BOX_WIDTH = 930; // 939;
        const int GROUP_BOX_SEPERATOR = 6;

        private static Dictionary<GroupBox, ETAGroup> gb_2_etag = new();

        public ETAGroup(TabPage tab_page)
        {
            group_box = new GroupBox();
            group_box.SuspendLayout();

            int existing_group_box_count = 0;
            for (int i = 0; i < tab_page.Controls.Count; i++)
            {
                if (tab_page.Controls[i] is GroupBox)
                    existing_group_box_count += 1;
            }

            group_box.Location = new Point(6, GROUP_BOX_START_Y + existing_group_box_count * (GROUP_BOX_HEIGHT + GROUP_BOX_SEPERATOR));
            group_box.Size = new Size(GROUP_BOX_WIDTH, GROUP_BOX_HEIGHT);
            group_box.TabStop = false;
            group_box.Text = "???";

            Button button_delete_eta = new Button();
            button_delete_eta.Location = new Point(862, 20);
            button_delete_eta.Name = "button4";
            button_delete_eta.Size = new Size(56, 23);
            button_delete_eta.TabIndex = 3;
            button_delete_eta.Text = "Delete";
            button_delete_eta.UseVisualStyleBackColor = true;
            button_delete_eta.Click += (s, ev) => Button_remove_eta(tab_page, group_box);

            text_box.Location = new Point(65, 21);
            text_box.Size = new Size(100, 23);
            text_box.KeyPress += TextBox_key_press_event;

            run_state_label.AutoSize = true;
            run_state_label.Location = new Point(6, 25);
            run_state_label.Size = new Size(53, 15);
            run_state_label.Text = "Name:";

            last_update_label.AutoSize = true;
            last_update_label.Location = new Point(204, 25);
            last_update_label.Size = new Size(102, 15);
            last_update_label.Text = "Last Update: 100.5";
            last_update_label.Hide();

            goal_label.AutoSize = true;
            goal_label.Location = new Point(345, 25);
            goal_label.Size = new Size(55, 15);
            goal_label.Text = "Goal: 100";
            goal_label.Hide();

            rate_label.AutoSize = true;
            rate_label.Location = new Point(439, 25);
            rate_label.Size = new Size(99, 15);
            rate_label.Text = "Rate: 106.3 / hour";
            rate_label.Hide();

            eta_label.AutoSize = true;
            eta_label.Location = new Point(577, 25);
            eta_label.Size = new Size(153, 15);
            eta_label.Text = "ETA: 53m 53s,  Thur 5:21 pm";
            eta_label.Hide();

            estemated_current_value_label.AutoSize = true;
            estemated_current_value_label.Location = new Point(751, 24);
            estemated_current_value_label.Size = new Size(92, 15);
            estemated_current_value_label.Text = "Est Value: 1056.4";
            estemated_current_value_label.Hide();

            group_box.ResumeLayout(false);
            group_box.PerformLayout();
            group_box.Controls.Add(text_box);
            group_box.Controls.Add(run_state_label);
            group_box.Controls.Add(last_update_label);
            group_box.Controls.Add(goal_label);
            group_box.Controls.Add(rate_label);
            group_box.Controls.Add(eta_label);
            group_box.Controls.Add(estemated_current_value_label);
            group_box.Controls.Add(button_delete_eta);

            tab_page.SuspendLayout();
            tab_page.Controls.Add(group_box);
            tab_page.ResumeLayout(false);
            tab_page.PerformLayout();
        }

        private void UpdateRunState(RunState new_state)
        {
            run_state = new_state;
            text_box.Text = String.Empty;

            run_state_label.SuspendLayout();
            switch (run_state)
            {
                case RunState.NeedsGoal:
                    run_state_label.Text = "Goal:";
                    break;
                case RunState.NeedsStartValue:
                    run_state_label.Text = "Initial Value:";
                    break;
                case RunState.Running:
                    run_state_label.Text = "Next Value:";
                    break;
                default:
                    throw new Exception($"Unknown run state update value: {run_state}");
            }
            run_state_label.ResumeLayout(true);
            run_state_label.PerformLayout();

            text_box.Location = new Point(run_state_label.Location.X + run_state_label.Size.Width + 5, text_box.Location.Y);
            last_update_label.Location = new Point(text_box.Location.X + text_box.Size.Width + 5, last_update_label.Location.Y);
        }

        private void TextBox_key_press_event(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Enter)
                return;

            var text = text_box.Text.Trim();
            if (String.IsNullOrWhiteSpace(text))
                return;

            e.Handled = true;

            if (run_state == RunState.NeedsName)
            {
                group_box.Text = text;
                UpdateRunState(RunState.NeedsGoal);
                return;
            }

            if (run_state == RunState.NeedsGoal)
            {
                if (!float.TryParse(text, out goal))
                {
                    System.Media.SystemSounds.Beep.Play();
                    return;
                }

                adaptive_eta = new(goal);

                goal_label.Text = $"Goal: {goal}";
                goal_label.Show();
                UpdateRunState(RunState.NeedsStartValue);
                return;
            }

            if (run_state == RunState.NeedsStartValue)
            {
                if (!float.TryParse(text, out float value))
                {
                    System.Media.SystemSounds.Beep.Play();
                    return;
                }

                adaptive_eta?.Add(value);

                last_update_label.Text = $"Last Update: {text}";
                last_update_label.Show();
                UpdateRunState(RunState.Running);
                return;
            }

            if (run_state == RunState.Running)
            {
                if (!float.TryParse(text, out float value))
                {
                    System.Media.SystemSounds.Beep.Play();
                    return;
                }

                if (adaptive_eta != null)
                {
                    adaptive_eta.Add(value);
                }

                UpdateEstimate(DateTime.Now);
                last_update_label.Text = $"Last Update: {text}";
                UpdateRunState(RunState.Running);
                return;
            }
        }
        public void UpdateEstimate(DateTime now)
        {
            if (adaptive_eta == null)
            {
                return;
            }

            (double currentAmount, DateTime eta, double amountPerSecond) = adaptive_eta.GetEstimate(now);
            if (double.IsNaN(amountPerSecond) || double.IsInfinity(amountPerSecond))
            {
                return;
            }

            estemated_current_value_label.Text = $"Est. Value: {currentAmount:F1}";
            estemated_current_value_label.Show();

            if (amountPerSecond == 0.0)
            {
                eta_label.Text = "Stalled";
            }
            else if (eta > now)
            {
                TimeSpan duration = eta - now;
                string duration_string = "???";
                if (duration.TotalSeconds < 90)
                {
                    duration_string = $"{Math.Round(duration.TotalSeconds)} seconds";
                }
                else if (duration.TotalSeconds < 60 * 60)
                {
                    int seconds = (int)Math.Round(duration.TotalSeconds);
                    duration_string = $"{seconds / 60}m {seconds % 60}s";
                }
                else if (duration.TotalSeconds < 60 * 60 * 24)
                {
                    int minutes = (int)Math.Round(duration.TotalSeconds / 60.0);
                    duration_string = $"{minutes / 60}h {minutes % 60}m";
                }
                else
                {
                    int minutes = (int)Math.Round(duration.TotalSeconds / 3600.0);
                    duration_string = $"{minutes / 24}d {minutes % 24}h";
                }

                string eta_string = "???";
                if (eta.DayOfYear == now.DayOfYear)
                {
                    eta_string = eta.ToString("hh:mm tt");
                }
                else if (duration.TotalDays < 6)
                {
                    eta_string = eta.ToString("ddd hh:mm tt");
                }
                else
                {
                    eta_string = eta.ToString("MMMM dd hh:mm tt");
                }

                eta_label.Text = $"ETA: {duration_string}, {eta_string}";
            }
            else
            {
                eta_label.Text = "Achieved";
            }
            eta_label.Show();

            string rate = String.Empty;
            if (amountPerSecond * 60 * 60 * 24 < 1)
            {
                rate = $"{amountPerSecond * 30 * 24 * 3600:F1} / month";
            }
            else if (amountPerSecond * 60 * 60 < 1)
            {
                rate = $"{amountPerSecond * 24 * 3600:F1} / day";
            }
            else if (amountPerSecond * 60 < 1)
            {
                rate = $"{amountPerSecond * 3600:F1} / hour";
            }
            else if (amountPerSecond < 1)
            {
                rate = $"{amountPerSecond * 60:F1} / min";
            }
            else
            {
                rate = $"{amountPerSecond:F1} / sec";
            }
            
            rate_label.Text = $"Rate: {rate}";
            rate_label.Show();
        }

        public static ETAGroup Create(TabPage tab_page)
        {
            ETAGroup eta_group = new ETAGroup(tab_page);
            gb_2_etag.Add(eta_group.group_box, eta_group);

            return eta_group;
        }

        private void Button_remove_eta(TabPage tab_page, GroupBox group_box)
        {
            tab_page.SuspendLayout();
            ETAGroup.Remove(tab_page, group_box);

            int existing_group_box_count = 0;
            for (int i = 0; i < tab_page.Controls.Count; i++)
            {
                if (tab_page.Controls[i] is GroupBox gbi)
                {
                    gbi.Location = new Point(6, GROUP_BOX_START_Y + existing_group_box_count * (GROUP_BOX_HEIGHT + GROUP_BOX_SEPERATOR));
                    existing_group_box_count += 1;
                }
            }

            tab_page.ResumeLayout(false);
            tab_page.PerformLayout();
        }

        public static void Remove(TabPage tab_page, GroupBox group_box)
        {
            if (gb_2_etag.TryGetValue(group_box, out ETAGroup? eta_group))
            {
                eta_group.run_state = RunState.Removed;
                gb_2_etag.Remove(group_box);
            }
            else
            {
                Console.WriteLine("ERROR: Could not find group box to remove");
            }
            
            tab_page.Controls.Remove(group_box);
        }

        public static void OnTick()
        {
            DateTime now = DateTime.Now;
            foreach (ETAGroup group in gb_2_etag.Values)
            {
                group.UpdateEstimate(now);
            }
        }
    }
}
