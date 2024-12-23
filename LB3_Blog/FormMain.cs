
using LB3_Blog.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace LB3_Blog
{
    public partial class FormMain : Form
    {
        private BlogContext? db;
        public FormMain()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.db = new BlogContext();
            this.db.Users.Load();
            this.dataGridViewUsers.DataSource = this.db.Users.Local.ToBindingList();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            this.db?.Dispose();
            this.db = null;
        }
        private void dataGridViewUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (this.db != null)
            {
                var user = (User)this.dataGridViewUsers.CurrentRow.DataBoundItem;
                if (user != null)
                {
                    this.db.Entry(user).Collection(e => e.Posts).Load();
                    this.dataGridViewPosts.DataSource = user.Posts;
                }
            }
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            this.db!.SaveChanges();
            this.dataGridViewUsers.Refresh();
            this.dataGridViewPosts.Refresh();
        }
    }
}
