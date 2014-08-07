using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DataLayer;
using DataLayer.Migrations;

namespace Exercise2
{
	public class BlogInitializer : MigrateDatabaseToLatestVersion<BlogContext, Configuration>
	{
	}
}