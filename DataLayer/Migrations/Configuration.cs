using System;
using System.Data.Entity.Migrations;
using DomainClasses.Entities;

namespace DataLayer.Migrations
{
    public class Configuration : DbMigrationsConfiguration<BlogContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(BlogContext context)
        {
			//Add the basic user profiles first
			context.UserProfiles.AddOrUpdate(a => a.Username,
				new UserProfile
				{
					Username = "Whit"
				},
				new UserProfile
				{
					Username = "Bill"
				},
				new UserProfile
				{
					Username = "Fred"
				},
				new UserProfile
				{
					Username = "Peter"
				},
				new UserProfile
				{
					Username = "Robert"
				});
			context.SaveChanges();

			//Add the password information for each of the users
			context.Passwords.AddOrUpdate(a => a.UserProfile,
				new Password
				{
					Passphrase = "35aa71ac2036722e5a08c5484e43401219ddf5637d8d2482490b20f8a3a26429",
					Salt = "NBT}<ORr]DV:^tVIRIwRO[NtjFl$K^IdPIO'$<HH!HVq>oy(NIlycqsVobVibomA=wospEkBpj$t![%?D{lySNQ)[BNkVqp.Bu!OM'I:$U:&h)YWS_AXLI-WSqv_Cz.j"
				},
				new Password
				{
					Passphrase = "7d9a787ff14e6e80e8e8b29543f015d97375d6b1f062417982bda5ea78b055d3",
					Salt = "'uI.mvP!uCG;dQAjT}B[w>q!^FSD$@^dIjpFGTGGx[:GMVNr*zob#yC{S>iEHTfJTDqN,j%BwkQ>i;A$O-PD)DaNu<]o^{dv;$Xn=qPqBI)j^_jp%@HfY(>,?b+EtYj]"
				},
				new Password
				{
					Passphrase = "ad0c15228622d9a8f6bf5951762e8e7d8c2a3b84d82ecfbb02dddd82c9620d49",
					Salt = "Jy$KPMJH,u>IC(vzDegaooUI,sSKbLF<+jA@EU@z>rqNwN(uasxd<Q'Rvduz:^,>aXCNHpgU{[FXHzz>afilTa[aUQuL(IQI#Kfs]s:g-O.R&+iy}MShMB(ZljTg.[_l"
				},
				new Password
				{
					Passphrase = "6d0e557126d243e4c26c6518bdb2b0e78c9074341237609f88ae992ad95a020c",
					Salt = "C&#BU&$<.+yL!nGJSV[zOQZghADvKh#!l#;zOPCoc&mRZ!Rj@SX[EI?hH_B(;p=J+MbFZp'Y{Dudj*,sh:sxcvnWJL]fAFRgUM,y,LMIVGpmw{Ag-Fq-Cl&B.RFq}gNA",
				},
				new Password
				{
					Passphrase = "d3d3235e1fa2154facb4fcab824713f9004aa2328372a6238c902890ac845e9d",
					Salt = "Q'rFI>JRfTM.@lQFT>OyLLP{TcoL^sSu{]]#p*}&F%PU({.)JXCgQ=rpwyXQnlt#Ol>jtVWgl&uaPCR}}ew^FjTo#S.P[QEeUK^Zr_GXYpakVkkUpK-wkOrx-Y*TDPzG"
				});
			context.SaveChanges();

			//Update the users with their passwords
			context.UserProfiles.Find(1).Passwords.Add(context.Passwords.Find(1));
			context.SaveChanges();

			context.UserProfiles.Find(2).Passwords.Add(context.Passwords.Find(2));
			context.SaveChanges();

			context.UserProfiles.Find(3).Passwords.Add(context.Passwords.Find(3));
			context.SaveChanges();

			context.UserProfiles.Find(4).Passwords.Add(context.Passwords.Find(4));
			context.SaveChanges();

			context.UserProfiles.Find(5).Passwords.Add(context.Passwords.Find(5));
			context.SaveChanges();

			//Add the various post data now
			context.BlogPosts.AddOrUpdate(a => a.Id,
				new BlogPost
				{
					Title = "Pork Chops!",
					Message = "<p style=\"border: 0px; margin-bottom: 24px; padding: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px; background-image: initial; background-attachment: initial; background-size: initial; background-origin: initial; background-clip: initial; background-position: initial; background-repeat: initial;\">Pork chop swine pork belly flank ham tri-tip kielbasa shoulder, turducken pancetta ribeye andouille. Turkey t-bone shoulder beef ribs, porchetta ground round corned beef ball tip fatback. Biltong boudin shank jerky rump, meatball chicken flank beef ribs. Doner salami ham hock shoulder venison beef ribs. Pork loin pork chop salami swine pork andouille venison corned beef.</p><p style=\"border: 0px; margin-bottom: 24px; padding: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px; background-image: initial; background-attachment: initial; background-size: initial; background-origin: initial; background-clip: initial; background-position: initial; background-repeat: initial;\">Pork short loin pancetta, ham hock bresaola shankle jerky leberkas pork chop kevin ground round biltong drumstick swine ball tip. Hamburger tail beef ribs sausage capicola boudin salami porchetta drumstick chicken kevin frankfurter cow pastrami. Pastrami tenderloin pig hamburger kielbasa salami, jerky jowl landjaeger kevin short loin fatback ham hock. Beef swine biltong strip steak salami pork chop venison flank meatball. Pork jerky pork chop jowl salami.</p>",
					CreatedOn = new DateTime(2014, 8, 6, 14, 53, 46)
				},
				new BlogPost
				{
					Title = "Tenderloin!",
					Message = "<span style=\"color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px;\">Shankle tenderloin doner strip steak, ham rump sirloin venison frankfurter. Biltong pastrami beef ribs tenderloin t-bone corned beef short ribs pork landjaeger shoulder jerky ground round shankle. Sirloin andouille bacon tail landjaeger. Pork tail leberkas boudin t-bone frankfurter turducken. Ham doner venison kielbasa frankfurter. Frankfurter salami jowl turkey porchetta.</span>",
					CreatedOn = new DateTime(2014, 8, 6, 14, 53, 59)
				},
				new BlogPost
				{
					Title = "Pork Tenderloin",
					Message = "<p style=\"border: 0px; margin-bottom: 24px; padding: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px; background-image: initial; background-attachment: initial; background-size: initial; background-origin: initial; background-clip: initial; background-position: initial; background-repeat: initial;\">Bacon ipsum dolor sit amet leberkas boudin ground round frankfurter. Exercitation shank ham swine. Eu jowl boudin, tenderloin quis strip steak sunt exercitation voluptate leberkas qui. Leberkas shank short ribs consectetur kevin. Aliquip drumstick pancetta chicken ham. Ullamco exercitation biltong magna. Commodo reprehenderit nulla tri-tip nostrud frankfurter.</p><p style=\"border: 0px; margin-bottom: 24px; padding: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px; background-image: initial; background-attachment: initial; background-size: initial; background-origin: initial; background-clip: initial; background-position: initial; background-repeat: initial;\">Exercitation labore in, ad shank in voluptate minim proident pancetta cillum. Ea ut pork chop ground round deserunt chicken do sint eu tail fatback aute qui incididunt sunt. Chicken dolore irure cow exercitation. Ad enim meatloaf tail. Aute nulla shoulder sirloin ut. Bacon landjaeger excepteur pork belly kielbasa dolor ullamco culpa capicola ea.</p><p style=\"border: 0px; margin-bottom: 24px; padding: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px; background-image: initial; background-attachment: initial; background-size: initial; background-origin: initial; background-clip: initial; background-position: initial; background-repeat: initial;\">Salami bacon ut mollit biltong sed dolore aliqua quis shoulder fatback venison prosciutto cupidatat. Ullamco beef ribs exercitation, elit kielbasa sausage shank sunt anim shankle aute turducken in magna venison. Ullamco enim velit tempor esse proident reprehenderit aute voluptate. Nulla kielbasa exercitation, laboris shoulder tongue ut. Pariatur ham hock drumstick pork ut swine biltong dolor aliquip ut tempor ad leberkas jowl.</p>",
					CreatedOn = new DateTime(2014, 8, 6, 15, 13, 10)
				},
				new BlogPost
				{
					Title = "Short Ribs!",
					Message = "<p style=\"border: 0px; margin-bottom: 24px; padding: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px; background-image: initial; background-attachment: initial; background-size: initial; background-origin: initial; background-clip: initial; background-position: initial; background-repeat: initial;\">Bacon ipsum dolor sit amet shankle frankfurter jerky tongue, jowl turducken biltong spare ribs sirloin pork chop. Strip steak chuck corned beef fatback short ribs shankle. Ham hock hamburger landjaeger tail chuck. Strip steak hamburger landjaeger, boudin rump ham hock shank chicken. Pancetta shankle kielbasa brisket beef ribs sausage spare ribs prosciutto.</p><p style=\"border: 0px; margin-bottom: 24px; padding: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px; background-image: initial; background-attachment: initial; background-size: initial; background-origin: initial; background-clip: initial; background-position: initial; background-repeat: initial;\">Ribeye kevin ham capicola ham hock jerky sirloin venison beef prosciutto shank boudin turducken short loin. Tenderloin jowl pastrami, bresaola corned beef turducken jerky andouille pork chop strip steak pig ground round. Ball tip pig cow short ribs, ham kevin doner jowl flank turkey. Andouille ham hock drumstick, sausage t-bone brisket prosciutto short loin meatloaf swine spare ribs shoulder tenderloin ham.</p><p style=\"border: 0px; margin-bottom: 24px; padding: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px; background-image: initial; background-attachment: initial; background-size: initial; background-origin: initial; background-clip: initial; background-position: initial; background-repeat: initial;\">Tenderloin andouille pastrami pork chop pork loin pork belly swine beef ribs strip steak frankfurter. Ground round bacon landjaeger, short ribs kevin beef beef ribs jowl spare ribs tongue meatloaf swine jerky doner. Cow t-bone landjaeger corned beef. Pork chop hamburger kevin, capicola venison corned beef pastrami turkey salami. Shank tenderloin ground round andouille ball tip meatloaf tail tongue, corned beef boudin bacon.</p><p style=\"border: 0px; margin-bottom: 24px; padding: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px; background-image: initial; background-attachment: initial; background-size: initial; background-origin: initial; background-clip: initial; background-position: initial; background-repeat: initial;\">Biltong shank tail tongue strip steak filet mignon swine rump kielbasa boudin. Pork meatball rump, strip steak tongue turducken venison brisket pork belly shankle tri-tip. Chicken pancetta doner, tenderloin tongue ball tip frankfurter cow pig ham capicola bresaola. Swine capicola ball tip tongue kielbasa, pancetta bacon ham pork belly tri-tip.</p><p style=\"border: 0px; margin-bottom: 24px; padding: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px; background-image: initial; background-attachment: initial; background-size: initial; background-origin: initial; background-clip: initial; background-position: initial; background-repeat: initial;\">Short ribs capicola meatball, ham ham hock shank short loin tongue andouille. Rump kielbasa beef ribs, tail salami ribeye prosciutto strip steak pastrami tri-tip turkey jowl tongue. Sirloin tenderloin frankfurter spare ribs swine doner ribeye pork belly cow capicola pastrami flank corned beef fatback. Biltong pork chop meatloaf short ribs chuck jowl sirloin ball tip tri-tip short loin tongue ribeye beef pastrami leberkas. Shoulder tenderloin turkey strip steak. Landjaeger prosciutto hamburger, turducken capicola bacon ham hock.</p>",
					CreatedOn = new DateTime(2014, 8, 7, 9, 10, 34)
				},
				new BlogPost
				{
					Title = "Pastrami",
					Message = "<span style=\"color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px;\">Tenderloin andouille pastrami pork chop pork loin pork belly swine beef ribs strip steak frankfurter. Ground round bacon landjaeger, short ribs kevin beef beef ribs jowl spare ribs tongue meatloaf swine jerky doner. Cow t-bone landjaeger corned beef. Pork chop hamburger kevin, capicola venison corned beef pastrami turkey salami. Shank tenderloin ground round andouille ball tip meatloaf tail tongue, corned beef boudin bacon.</span>",
					CreatedOn = new DateTime(2014, 8, 6, 9, 12, 02)
				},
				new BlogPost
				{
					Title = "Sirloin",
					Message =
						"<span style=\"color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px;\">Short ribs capicola meatball, ham ham hock shank short loin tongue andouille. Rump kielbasa beef ribs, tail salami ribeye prosciutto strip steak pastrami tri-tip turkey jowl tongue. Sirloin tenderloin frankfurter spare ribs swine doner ribeye pork belly cow capicola pastrami flank corned beef fatback. Biltong pork chop meatloaf short ribs chuck jowl sirloin ball tip tri-tip short loin tongue ribeye beef pastrami leberkas. Shoulder tenderloin turkey strip steak. Landjaeger prosciutto hamburger, turducken capicola bacon ham hock.</span>",
					CreatedOn = new DateTime(2014, 8, 7, 6, 12, 45)
				},
				new BlogPost
				{
					Title = "Hamburger",
					Message =
						"<p style=\"border: 0px; margin-bottom: 24px; padding: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px; background-image: initial; background-attachment: initial; background-size: initial; background-origin: initial; background-clip: initial; background-position: initial; background-repeat: initial;\">Bacon ipsum dolor sit amet beef ribs short loin chuck capicola. Sausage kevin tri-tip ham hock. Turkey swine beef sausage tongue landjaeger, leberkas beef ribs ham drumstick ground round. Turkey porchetta chuck ground round jowl, short loin pancetta cow shoulder short ribs shank bresaola ham bacon meatball. Pork loin chuck turducken flank short ribs salami pork chop corned beef meatball pastrami. Chuck shank capicola pork, swine landjaeger pastrami doner.</p><p style=\"border: 0px; margin-bottom: 24px; padding: 0px; vertical-align: baseline; color: rgb(51, 51, 51); font-family: Georgia, 'Bitstream Charter', serif; font-size: 16px; line-height: 24px; background-image: initial; background-attachment: initial; background-size: initial; background-origin: initial; background-clip: initial; background-position: initial; background-repeat: initial;\">Beef sausage tongue beef ribs boudin strip steak hamburger chuck tri-tip short loin capicola short ribs. Kielbasa tri-tip ball tip bresaola. Porchetta biltong strip steak tenderloin pancetta cow corned beef ball tip shankle filet mignon meatloaf. Leberkas drumstick pancetta pork belly chuck bacon doner turducken andouille corned beef tail biltong ground round.</p>",
					CreatedOn = new DateTime(2014, 8, 7, 9, 14, 27)
				});
			context.SaveChanges();

			//Update the users with their blog posts
			context.UserProfiles.Find(4).BlogPosts.Add(context.BlogPosts.Find(1));
			context.UserProfiles.Find(4).BlogPosts.Add(context.BlogPosts.Find(2));
			context.UserProfiles.Find(2).BlogPosts.Add(context.BlogPosts.Find(3));
			context.UserProfiles.Find(5).BlogPosts.Add(context.BlogPosts.Find(4));
			context.UserProfiles.Find(5).BlogPosts.Add(context.BlogPosts.Find(5));
			context.UserProfiles.Find(1).BlogPosts.Add(context.BlogPosts.Find(6));
			context.UserProfiles.Find(4).BlogPosts.Add(context.BlogPosts.Find(7));
			context.SaveChanges();

			//Now, create the comments
			context.Comments.AddOrUpdate(a => a.Id,
				new Comment
				{
					Body = "Man, I would really enjoy some pork chops right about now. Good thinking!",
					CreatedOn = new DateTime(2014, 8, 6, 15, 17, 14)
				},
				new Comment
				{
					Body = "The bigger question is what sorts of sides I should have with my pork chops....",
					CreatedOn = new DateTime(2014, 8, 7, 12, 14, 07)
				},
				new Comment
				{
					Body = "I love short ribs!",
					CreatedOn = new DateTime(2014, 8, 7, 12, 15, 11)
				});
			context.SaveChanges();

			//Bind these comments to the users
			context.UserProfiles.Find(1).Comments.Add(context.Comments.Find(1));
			context.UserProfiles.Find(2).Comments.Add(context.Comments.Find(2));
			context.UserProfiles.Find(5).Comments.Add(context.Comments.Find(3));
			context.SaveChanges();

			//Bind these comments to the posts
			context.BlogPosts.Find(3).Comments.Add(context.Comments.Find(1));
			context.BlogPosts.Find(3).Comments.Add(context.Comments.Find(2));
			context.BlogPosts.Find(6).Comments.Add(context.Comments.Find(3));
			context.SaveChanges();
        }
    }
}
