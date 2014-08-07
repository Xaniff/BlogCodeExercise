using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DataLayer;
using Exercise2.Migrations;

namespace Exercise2
{
	//public class BlogInitializer : DropCreateDatabaseAlways<BlogContext> 
	public class BlogInitializer : MigrateDatabaseToLatestVersion<BlogContext, Configuration>
	{
	}
}