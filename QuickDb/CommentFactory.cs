using BLL.Entity;
using System;
using System.Collections.Generic;

namespace QuickDb
{
    static class CommentFactory
    {
        internal static List<Comment> Comments { get; set; }

        internal static void GenerateComments()
        {
            GenerateCommentList();
            foreach (var item in Comments)
            {
                ArticleFactory.Article1.Comments.Add(item);
            }
        }

        private static void GenerateCommentList()
        {
            Comments = new List<Comment>();

            Comment comment = CreateComment(UserFactory.Admin);
            Comment replyComment1 = CreateComment(UserFactory.UserNumberOne, UserFactory.Admin);
            Comment replyComment2 = CreateComment(UserFactory.UserNumberTwo, UserFactory.UserNumberOne);

            comment.Replys.Add(replyComment1);
            comment.Replys.Add(replyComment2);

            Comments.Add(comment);
            Comments.Add(replyComment1);
            Comments.Add(replyComment2);

            for (int i = 0; i < 2; i++)
            {
                Comments.Add(CreateComment(UserFactory.Admin));
            }
        }

        private static Comment CreateComment(User author, User replyUser = null)
        {
            return new Comment
            {
                Author = author,
                Body = "演示评论",
                PublishTime = DateTime.Now,
                ReplyUser = replyUser,
            };
        }
    }
}
