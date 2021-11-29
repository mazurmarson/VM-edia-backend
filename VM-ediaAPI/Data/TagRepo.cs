using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VM_ediaAPI.Models;

namespace VM_ediaAPI.Data
{
    public class TagRepo : GenRepo, ITagRepo
    {
        private readonly DataContext _context;
        public TagRepo(DataContext context):base(context)
        {
            _context = context;
        }

        public async Task<List<PostTag>> GetTagsAndUpdateAmmountAdding(string description)
        {
             string pattern = @"\B#([a-z0-9]{2,})(?![~!@#$%^&*()=+_`\-\|\/'\[\]\{\}]|[?.,]*\w)";
           
            RegexOptions options = RegexOptions.IgnoreCase;
             Regex rg = new Regex(pattern, options);
            MatchCollection matchedTags = rg.Matches(description);
            List<PostTag> tags = new List<PostTag>();
            for(int count = 0; count < matchedTags.Count; count++)
            {
                bool tagExist = _context.Tags.Any(x => x.Content == matchedTags[count].Value);
                if(tagExist)
                {
                    var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Content == matchedTags[count].Value);
                    tag.Amount ++;
                    
                    _context.Tags.Update(tag);
                    PostTag postTag = new PostTag{
                        Tag = tag
                    };
                    tags.Add(postTag);
                }
                else
                {
                    Tag tag = new Tag()
                    {
                        Content = matchedTags[count].Value,
                        Amount = 1
                    };
                    _context.Tags.Add(tag);
                    PostTag postTag = new PostTag{
                        Tag = tag
                    };
                    tags.Add(postTag);
                    
                }
            }
            await _context.SaveChangesAsync();
            return tags;
        }

        public async Task<bool> TagAmmountDecrementationByPostId(int postId)
        {
            string description = await _context.Posts.Where(x => x.Id == postId).Select(u => u.Description).FirstOrDefaultAsync();
            if(description == null)
            {
                return false;
            }

            string pattern = @"\B#([a-z0-9]{2,})(?![~!@#$%^&*()=+_`\-\|\/'\[\]\{\}]|[?.,]*\w)";
           
            RegexOptions options = RegexOptions.IgnoreCase;
             Regex rg = new Regex(pattern, options);
            MatchCollection matchedTags = rg.Matches(description);
            List<Tag> tags = new List<Tag>();
            for(int count = 0; count < matchedTags.Count; count++)
            {
                bool tagExist = _context.Tags.Any(x => x.Content == matchedTags[count].Value);
                if(tagExist)
                {
                    var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Content == matchedTags[count].Value);
                    tag.Amount --;
                    _context.Tags.Update(tag);
                    tags.Add(tag);
                }


            }
            await _context.SaveChangesAsync();
            return true;


        }

        public async Task<bool> TagAmmountDecrementation2(int postId)
        {
            string description = await _context.Posts.Where(x => x.Id == postId).Select(u => u.Description).FirstOrDefaultAsync();
            if(description == null)
            {
                return false;
            }

            string pattern = @"\B#([a-z0-9]{2,})(?![~!@#$%^&*()=+_`\-\|\/'\[\]\{\}]|[?.,]*\w)";
           
            RegexOptions options = RegexOptions.IgnoreCase;
             Regex rg = new Regex(pattern, options);
            MatchCollection matchedTags = rg.Matches(description);
            List<Tag> tags = new List<Tag>();
            for(int count = 0; count < matchedTags.Count; count++)
            {
                bool tagExist = _context.Tags.Any(x => x.Content == matchedTags[count].Value);
                if(tagExist)
                {
                    var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Content == matchedTags[count].Value);
                    
                    tag.Amount --;
                    _context.Tags.Update(tag);
                    tags.Add(tag);
                }
                

            }
            await _context.SaveChangesAsync();
            return true;


        }


         public async Task<List<PostTag>> UpdatePostTags(string description, int postId)
        {
             string pattern = @"\B#([a-z0-9]{2,})(?![~!@#$%^&*()=+_`\-\|\/'\[\]\{\}]|[?.,]*\w)";
           
            RegexOptions options = RegexOptions.IgnoreCase;
             Regex rg = new Regex(pattern, options);
            MatchCollection matchedTags = rg.Matches(description);
            List<PostTag> tags = new List<PostTag>();
            for(int count = 0; count < matchedTags.Count; count++)
            {
                bool tagExist = _context.Tags.Any(x => x.Content == matchedTags[count].Value);
                if(tagExist)
                {
                    var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Content == matchedTags[count].Value);
                    tag.Amount ++;
                    
                    _context.Tags.Update(tag);
                    PostTag postTag = new PostTag{
                        Tag = tag,
                        PostId = postId
                    };
                    _context.PostTags.Add(postTag);
                    tags.Add(postTag);
                }
                else
                {
                    Tag tag = new Tag()
                    {
                        Content = matchedTags[count].Value,
                        Amount = 1
                    };
                    _context.Tags.Add(tag);
                    PostTag postTag = new PostTag{
                        Tag = tag,
                        PostId = postId
                    };
                    _context.PostTags.Add(postTag);
                    tags.Add(postTag);
                    
                }
            }
            await _context.SaveChangesAsync();
            return tags;
        }

        public async Task<bool> TagAmmountDecrementationUpdateByPostId(int postId)
        {
                        string description = await _context.Posts.Where(x => x.Id == postId).Select(u => u.Description).FirstOrDefaultAsync();
            if(description == null)
            {
                return false;
            }

            string pattern = @"\B#([a-z0-9]{2,})(?![~!@#$%^&*()=+_`\-\|\/'\[\]\{\}]|[?.,]*\w)";
            //var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == postId);
           
            RegexOptions options = RegexOptions.IgnoreCase;
             Regex rg = new Regex(pattern, options);
            MatchCollection matchedTags = rg.Matches(description);
            List<Tag> tags = new List<Tag>();
            for(int count = 0; count < matchedTags.Count; count++)
            {
                bool tagExist = _context.Tags.Any(x => x.Content == matchedTags[count].Value);
                if(tagExist)
                {
                    var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Content == matchedTags[count].Value);
                    tag.Amount --;
                    _context.Tags.Update(tag);
                    PostTag postTag = new PostTag
                    {
                        PostId = postId,
                        Tag = tag
                    };
                    _context.PostTags.Remove(postTag);
                    tags.Add(tag);
                }


            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}