using System;
using System.Collections.Generic;
using System.IO;
using AuthDLL;
using System.Windows.Forms;

namespace MenuDLL
{
    public class MenuItem
    {
        public string Text { get; set; }
        public string MethodName { get; set; }
        public int Level { get; set; }
        public List<MenuItem> Children { get; set; } = new List<MenuItem>();
    }

    public class MenuBuilder
    {
        private List<MenuItem> menuStructure = new List<MenuItem>();
        private AuthDLL.AuthUser currentUser;

        public MenuBuilder(string menuFile = "menu.txt", AuthDLL.AuthUser user = null)
        {
            currentUser = user;
            LoadMenuStructure(menuFile);
        }

        private void LoadMenuStructure(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException($"Menu file {filename} not found");

            var lines = File.ReadAllLines(filename);
            var stack = new Stack<MenuItem>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var parts = line.Split(new[] { ' ' }, 3);
                if (parts.Length < 2) continue;

                int level = int.Parse(parts[0]);
                string text = parts[1];
                string method = parts.Length > 2 ? parts[2] : null;

                var item = new MenuItem { Level = level, Text = text, MethodName = method };

                if (currentUser != null && currentUser.MenuPermissions.ContainsKey(text))
                {
                    int status = currentUser.MenuPermissions[text];
                    if (status == 2) continue; // Skip if hidden
                }

                if (level == 0)
                {
                    menuStructure.Add(item);
                    stack.Clear();
                    stack.Push(item);
                }
                else
                {
                    while (stack.Count > level) stack.Pop();
                    if (stack.Count > 0)
                    {
                        stack.Peek().Children.Add(item);
                        stack.Push(item);
                    }
                }
            }
        }

        public void BuildMenu(MenuStrip menuStrip, EventHandler clickHandler)
        {
            menuStrip.Items.Clear();
            BuildMenuItems(menuStructure, menuStrip.Items, clickHandler);
        }

        private void BuildMenuItems(List<MenuItem> items, ToolStripItemCollection parent, EventHandler clickHandler)
        {
            foreach (var item in items)
            {
                if (currentUser != null && currentUser.MenuPermissions.ContainsKey(item.Text) &&
                    currentUser.MenuPermissions[item.Text] == 1)
                {
                  
                    var menuItem = new ToolStripMenuItem(item.Text) { Enabled = false };
                    parent.Add(menuItem);
                    if (item.Children.Count > 0)
                    {
                        BuildMenuItems(item.Children, menuItem.DropDownItems, clickHandler);
                    }
                }
                else
                {
                    var menuItem = new ToolStripMenuItem(item.Text);
                    if (!string.IsNullOrEmpty(item.MethodName))
                    {
                        menuItem.Tag = item.MethodName;
                        menuItem.Click += clickHandler;
                    }
                    parent.Add(menuItem);
                    if (item.Children.Count > 0)
                    {
                        BuildMenuItems(item.Children, menuItem.DropDownItems, clickHandler);
                    }
                }
            }
        }
    }
}
