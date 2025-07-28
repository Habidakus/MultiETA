using MultiETA;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MultiETA
{
    public class Category
    {
        private static List<Category> categories = new List<Category>();

        private TabPage tab_page = new TabPage();
        private Button button_sort = new Button();
        private Dictionary<GroupBox, ETAGroup> gb_2_etag = new();
        public int NextCreationOrder { get { return gb_2_etag.Any() ? gb_2_etag.Values.Max(a => a.creation_order) + 1 : 1; } }

        private JsonObject AsJson()
        {
            JsonArray eta_array = new JsonArray();
            foreach (var eta_group in gb_2_etag.Values)
            {
                eta_array.Add(eta_group.AsJson());
            }

            JsonObject json = new JsonObject
            {
                { "Name", tab_page.Text },
                { "ETAs", eta_array },
            };
            return json;
        }

        private void Load(JsonElement category_el)
        {
            JsonElement eta_array = category_el.GetProperty("ETAs");
            foreach (JsonElement eta_el in eta_array.EnumerateArray())
            {
                ETAGroup eta_group = CreateETAGroup(tab_page);
                eta_group.Load(eta_el);
            }

            if (category_el.TryGetProperty("Name", out JsonElement name_el))
            {
                if (name_el.GetString() is String name_string)
                {
                    tab_page.Text = name_string;
                }
            }
        }

        public ETAGroup CreateETAGroup(TabPage tab_page)
        {
            ETAGroup eta_group = new ETAGroup(this, tab_page);
            gb_2_etag.Add(eta_group.group_box, eta_group);

            return eta_group;
        }

        public void RemoveETAGroup(TabPage tab_page, GroupBox group_box)
        {
            if (gb_2_etag.TryGetValue(group_box, out ETAGroup? eta_group))
            {
                eta_group.MarkRemoved();
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
            foreach (Category category in categories)
            {
                // #TODO: Only run this on the currently visible tab
                category.UpdateEstimates(now);
            }
        }

        private void UpdateEstimates(DateTime now)
        { 
            foreach (ETAGroup group in gb_2_etag.Values)
            {
                group.UpdateEstimate(now);
            }
        }

        public Category(TabControl tab_control)
        {
            tab_page.SuspendLayout();

            Button button_add_eta = new Button();
            button_add_eta.AutoSize = true;
            button_add_eta.Location = new Point(12, 11);
            button_add_eta.Size = new Size(62, 23);
            button_add_eta.Text = "Add ETA";
            button_add_eta.UseVisualStyleBackColor = true;
            button_add_eta.Click += (s, ev) => Button_add_eta_Click(tab_page);

            button_sort.AutoSize = true;
            button_sort.Location = new Point(90, 11);
            button_sort.Size = new Size(62, 23);
            button_sort.Text = "Sort";
            button_sort.UseVisualStyleBackColor = true;
            button_sort.Click += (s, ev) => Button_sort_Click(tab_page);

            Button button_remove_category1 = new Button();
            tab_page.Controls.Add(button_remove_category1);
            button_remove_category1.AutoSize = true;
            button_remove_category1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_remove_category1.Location = new Point(837, 11);
            button_remove_category1.Size = new Size(110, 23);
            button_remove_category1.Text = "Remove Category";
            button_remove_category1.UseVisualStyleBackColor = true;
            button_remove_category1.Click += (s, ev) => Button_remove_category_Click(tab_control, tab_page);

            tab_page.AutoScroll = true;
            tab_page.Controls.Add(button_sort);
            tab_page.Controls.Add(button_add_eta);
            tab_page.Location = new Point(4, 24);
            tab_page.Padding = new Padding(3);
            tab_page.Size = new Size(1096, 776);
            tab_page.TabIndex = 0;
            tab_page.Text = "???";
            tab_page.UseVisualStyleBackColor = true;

            tab_page.ResumeLayout(false);
            tab_page.PerformLayout();

            tab_control.Controls.Add(tab_page);
        }

        private void Button_remove_category_Click(TabControl tab_control, TabPage tab_page)
        {
            categories.Remove(this);
            tab_control.Controls.Remove(tab_page);
        }

        private void Button_add_eta_Click(TabPage tab_page)
        {
            ETAGroup _ = CreateETAGroup(tab_page);
        }

        private bool sort_by_eta;
        private void Button_sort_Click(TabPage tab_page)
        {
            List<GroupBox> group_boxes = gb_2_etag.Keys.ToList();
            sort_by_eta = !sort_by_eta;
            button_sort.SuspendLayout();
            if (sort_by_eta)
            {
                DateTime now = DateTime.Now;
                group_boxes.Sort((right,left) => gb_2_etag[right].CompareTo(now, gb_2_etag[left]));
                button_sort.Text = "Sort by creation order";
            }
            else
            {
                group_boxes.Sort((right, left) => gb_2_etag[right].creation_order.CompareTo(gb_2_etag[left].creation_order));
                button_sort.Text = "Sort by ETA";
            }
            button_sort.ResumeLayout(false);
            button_sort.PerformLayout();

            int index = 0;
            foreach (GroupBox group in group_boxes)
            {
                gb_2_etag[group].SetDisplaySlot(index);
                ++index;
            }
        }

        public static Category Create(TabControl tab_control)
        {
            Category category = new Category(tab_control);
            categories.Add(category);
            return category;
        }

        internal static SaveOnDispose GenerateSaveOnDispose()
        {
            return new SaveOnDispose(GetSaveDataPath());
        }

        internal static string GetSaveDataPath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dataStorageFolder = Path.Combine(appDataPath, "Habidakus", "MultiETA");
            if (!Directory.Exists(dataStorageFolder))
            {
                Directory.CreateDirectory(dataStorageFolder);
            }

            return Path.Combine(dataStorageFolder, "data.json");
        }

        internal static JsonObject AllAsJson()
        {
            JsonArray categories_array = new();
            foreach (Category category in categories)
            {
                categories_array.Add(category.AsJson());
            }

            JsonObject root = new JsonObject();
            root.Add("Categories", categories_array);

            return root;
        }

        internal static void Load(TabControl tab_control, JsonElement root_el)
        {
            JsonElement category_array = root_el.GetProperty("Categories");
            foreach (JsonElement array_el in category_array.EnumerateArray())
            {
                Category category = new Category(tab_control);
                category.Load(array_el);
                categories.Add(category);
            }
        }
    }
}

public class SaveOnDispose : IDisposable
{
    private string path { get; }
    public SaveOnDispose(string path)
    {
        this.path = path;
    }

    public void Dispose()
    {
        JsonWriterOptions options = new JsonWriterOptions { Indented = true };
        using FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write);
        using Utf8JsonWriter writer = new Utf8JsonWriter(stream, options);
        Category.AllAsJson().WriteTo(writer);
    }
}
