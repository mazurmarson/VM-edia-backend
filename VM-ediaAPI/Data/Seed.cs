using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public class Seed
    {
        DataContext _dataContext;
        IPasswordHasher<User> _passwordHasher;
        public Seed(DataContext dataContext, IPasswordHasher<User> passwordHasher)
        {
            _dataContext = dataContext;
            _passwordHasher = passwordHasher;
        }

        public void SeedUsers()
        {
            var userData = File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);


            foreach(var user in users)
            {
                    var hashedPassword = _passwordHasher.HashPassword(user, "password");
                    user.PasswordHash = hashedPassword;

                    user.Login = user.Login.ToLower();

                    _dataContext.Users.Add(user);
                    
            }
            _dataContext.SaveChanges();
        }

        public void SeedPosts()
        {
            var postsData = File.ReadAllText("Data/PostSeedData.json");
            var posts = JsonConvert.DeserializeObject<List<Post>>(postsData);

            foreach(var post in posts)
            {
                _dataContext.Posts.Add(post);
            }
            _dataContext.SaveChanges();
        }

        public void SeedComments()
        {
            var commentsData = File.ReadAllText("Data/CommentSeedData.json");
            var comments = JsonConvert.DeserializeObject<List<Comment>>(commentsData);

            foreach(var comment in comments)
            {
                _dataContext.Comments.Add(comment);
            }
            _dataContext.SaveChanges();
        }

        public void SeedReactions()
        {
            var reactionsData = File.ReadAllText("Data/ReactionSeedData.json");
            var reactions = JsonConvert.DeserializeObject<List<Reaction>>(reactionsData);

            foreach(var reaction in reactions)
            {
                _dataContext.Reactions.Add(reaction);
            }
            _dataContext.SaveChanges();
        }

        public void SeedFollow()
        {
            var followsData = File.ReadAllText("Data/FollowSeedData.json");
            var follows = JsonConvert.DeserializeObject<List<Follow>>(followsData);

            foreach(var follow in follows)
            {
                _dataContext.Follows.Add(follow);
            }
            _dataContext.SaveChanges();
        }
    }
}

//  '{{repeat(30)}}', Tworzenie uzytkownikow
//   {
//     firstName: '{{firstName()}}',
//     lastName: '{{surname()}}',
//     login: function(tags){
//     return this.firstName + tags.integer(1,100);
//     },
//     mail: function(tags){
//     return this.login + '@o2.pl';
//     },
//     dateOfBirth: '{{date(new Date(1990 - 2015), new Date(), "YYYY-MM-ddThh:mm:ss")}}',
//     description: '{{lorem(1, "sentence")}}',
//     mainPhotoUrl: function(tags){
//     var photos = ['https://res.cloudinary.com/mazicloud/image/upload/v1612782934/pilka_nozna_auhio3.png',
//                  'https://res.cloudinary.com/mazicloud/image/upload/v1612782933/siatkowka_xocvz2.png',
//                  'https://res.cloudinary.com/mazicloud/image/upload/v1612782932/basketball_cmvnte.png',
//                  'https://res.cloudinary.com/mazicloud/image/upload/v1612782932/pletka_pkk7ze.png',
//                  'https://res.cloudinary.com/mazicloud/image/upload/v1612782930/hokej_w0kd9f.png',
//                  'https://res.cloudinary.com/mazicloud/image/upload/v1612782930/pad_jmzggj.png',
//         'https://res.cloudinary.com/mazicloud/image/upload/v1612782929/bieg_icib7u.png'];
//       return photos[tags.integer(0, photos.length -1)];
//     }
//   }
// ]

// [
//   '{{repeat(10)}}',
//   {
//     userId: '{{integer(1,30)}}',
//     description: '{{lorem(2, "sentences")}}',
//     createAt: '{{date(new Date(2021, 0, 1), new Date(), "YYYY-MM-ddThh:mm:ss")}}',
//     photos: [
//       {
        
//         urlAdress: function(num) {
//       return 'https://randomuser.me/api/portraits/men/' + num.integer(1,99) +'.jpg';     
//         }},
        
//               {
        
//         urlAdress: function(num) {
//       return 'https://randomuser.me/api/portraits/men/' + num.integer(1,99) +'.jpg';     
//         }}
      
      
//       ]
    
//   }
// ]


// [
//   '{{repeat(30)}}',
//   {
//     userId: '{{integer(1,30)}}',
//     description: '{{lorem(2, "sentences")}}',
//     createAt: '{{date(new Date(2021, 0, 1), new Date(), "YYYY-MM-ddThh:mm:ss")}}',
//     Photos: [
//       {
//         urlAdress: function(num) {
//       return 'https://randomuser.me/api/portraits/women/' + num.integer(1,99) +'.jpg';     
//         }
//       }
      
//       ]
    
//   }
// ]


// [
//   '{{repeat(120)}}',
//   {
//     userId: '{{integer(1,30)}}',
//     postId: '{{integer(1,60)}}',
//     content: '{{lorem(1, "sentences")}}',
//     createAt: '{{date(new Date(2021, 0, 1), new Date(), "YYYY-MM-ddThh:mm:ss")}}'

//   }
// ]


// [
//   '{{repeat(200)}}',
//   {
//     userId: '{{integer(1,30)}}',
//     postId: '{{integer(1,60)}}',
//     isPositive: '{{bool()}}'

//   }
// ]

// [
//   '{{repeat(200)}}',
//   {
//     followerId: '{{integer(1,30)}}',
//     followedUserId: '{{integer(1,60)}}'


//   }
// ]