using System.Data.SqlClient;

namespace NimapCrud.Models
{
    public class CategoryCrud
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        IConfiguration configuration;

        public CategoryCrud(IConfiguration configuration)
        {
            this.configuration = configuration;
            con = new SqlConnection(this.configuration.GetConnectionString("DefaultConnection"));
        }
        //--------CRUD OPERATIONS
        //1>list of category
        public List<Category> GetCategories()
        {
            List<Category> list = new List<Category>();
            cmd=new SqlCommand("select * from category",con);
            con.Open();
            reader= cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Category category = new Category();
                    category.CategoryId = Convert.ToInt32(reader["categoryid"]);
                    category.CategoryName = reader["categoryname"].ToString();
                    list.Add(category);
                }
            }
            con.Close();
            return list;
        }
        //2> Add Category
        public int AddCategory(Category category)
        {
            int result = 0;
            string qry = "insert into Category(categoryname) values(@categoryname)";
            cmd= new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@categoryname", category.CategoryName);
            con.Open();
            result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        //3> get category by id
        public Category GetCategoryById(int categoryid)
        {
            Category category = new Category();
            cmd = new SqlCommand("select * from Category where categoryid=@categoryid", con);
            cmd.Parameters.AddWithValue("@categoryid", categoryid);
            con.Open();
            reader=cmd.ExecuteReader();
            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    category.CategoryId = Convert.ToInt32(reader["categoryid"]);
                    category.CategoryName = reader["categoryname"].ToString();

                }
            }
            con.Close(); 
            return category;
        }
        //4>Update category
        public int UpdateCategory(Category category)
        {
            int result = 0;
            string qry = "update Category set categoryname=@categoryname where categoryid=@categoryid";
            cmd= new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@categoryname", category.CategoryName);
            con.Open();
            result= cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        //Delete category
        public int DeleteCategory(int categoryid)
        {
            int result = 0;
            string qry = "delete from Category where categoryid=@categoryid";
            cmd= new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@categoryid", categoryid);
            con.Open();
            result= cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }

}
