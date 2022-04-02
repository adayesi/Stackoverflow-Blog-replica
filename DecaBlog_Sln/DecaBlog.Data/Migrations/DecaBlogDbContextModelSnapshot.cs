﻿// <auto-generated />
using System;
using DecaBlog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DecaBlog.Data.Migrations
{
    [DbContext(typeof(DecaBlogDbContext))]
    partial class DecaBlogDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.13");

            modelBuilder.Entity("DecaBlog.Models.Address", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("DecaBlog.Models.Article", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ArticleText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ArticleTopicId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DatePublished")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPublished")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Keywords")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("PublisherId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SubTopic")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT")
                        .HasColumnName("ContributorId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleTopicId");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("DecaBlog.Models.ArticleTopic", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Abstract")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("PublicId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasMaxLength(150)
                        .HasColumnType("TEXT")
                        .HasColumnName("AuthorId");

                    b.Property<string>("UserId1")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("ArticleTopics");
                });

            modelBuilder.Entity("DecaBlog.Models.Category", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DecaBlog.Models.CommentVote", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("VoterId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CommentId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Vote")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id", "VoterId", "CommentId");

                    b.HasIndex("CommentId");

                    b.HasIndex("VoterId");

                    b.ToTable("CommentVotes");
                });

            modelBuilder.Entity("DecaBlog.Models.Invitee", b =>
                {
                    b.Property<string>("InviteeId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("InvitedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("InviterToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SquadId")
                        .HasColumnType("TEXT");

                    b.Property<string>("StackId")
                        .HasColumnType("TEXT");

                    b.HasKey("InviteeId");

                    b.ToTable("Invitees");
                });

            modelBuilder.Entity("DecaBlog.Models.Notification", b =>
                {
                    b.Property<string>("ActivityId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ActionPerformedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeprecated")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRead")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NoticeText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("DecaBlog.Models.Photo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsMain")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PublicId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("DecaBlog.Models.Squad", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("GradDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsGraduated")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Squads");
                });

            modelBuilder.Entity("DecaBlog.Models.Stack", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Stacks");
                });

            modelBuilder.Entity("DecaBlog.Models.SupportingEmail", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("SupportingEmail");
                });

            modelBuilder.Entity("DecaBlog.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .HasColumnType("TEXT");

                    b.Property<string>("InvitationToken")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PhotoPublicId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("DecaBlog.Models.UserComment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("CommentText")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CommenterId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("TopicId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CommenterId");

                    b.HasIndex("TopicId");

                    b.ToTable("UserComments");
                });

            modelBuilder.Entity("DecaBlog.Models.UserLike", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("LikerId")
                        .HasColumnType("TEXT");

                    b.Property<string>("TopicId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateUpdated")
                        .HasColumnType("TEXT");

                    b.HasKey("Id", "LikerId", "TopicId");

                    b.HasIndex("LikerId")
                        .IsUnique();

                    b.HasIndex("TopicId");

                    b.ToTable("UserLikes");
                });

            modelBuilder.Entity("DecaBlog.Models.UserSquad", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("SquadId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id", "UserId");

                    b.HasIndex("SquadId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSquad");
                });

            modelBuilder.Entity("DecaBlog.Models.UserStack", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("StackId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id", "UserId");

                    b.HasIndex("StackId");

                    b.HasIndex("UserId");

                    b.ToTable("UserStack");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("DecaBlog.Models.Address", b =>
                {
                    b.HasOne("DecaBlog.Models.User", "User")
                        .WithOne("Address")
                        .HasForeignKey("DecaBlog.Models.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DecaBlog.Models.Article", b =>
                {
                    b.HasOne("DecaBlog.Models.ArticleTopic", "ArticleTopic")
                        .WithMany("ArticleList")
                        .HasForeignKey("ArticleTopicId");

                    b.HasOne("DecaBlog.Models.User", "Contributor")
                        .WithMany("Articles")
                        .HasForeignKey("UserId");

                    b.Navigation("ArticleTopic");

                    b.Navigation("Contributor");
                });

            modelBuilder.Entity("DecaBlog.Models.ArticleTopic", b =>
                {
                    b.HasOne("DecaBlog.Models.Category", "Category")
                        .WithMany("ArticleTopics")
                        .HasForeignKey("CategoryId");

                    b.HasOne("DecaBlog.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.HasOne("DecaBlog.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1");

                    b.Navigation("Author");

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DecaBlog.Models.CommentVote", b =>
                {
                    b.HasOne("DecaBlog.Models.UserComment", "Comment")
                        .WithMany("Votes")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DecaBlog.Models.User", "Voter")
                        .WithMany()
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comment");

                    b.Navigation("Voter");
                });

            modelBuilder.Entity("DecaBlog.Models.Notification", b =>
                {
                    b.HasOne("DecaBlog.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DecaBlog.Models.Photo", b =>
                {
                    b.HasOne("DecaBlog.Models.User", "AppUser")
                        .WithMany("Photos")
                        .HasForeignKey("UserId");

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("DecaBlog.Models.SupportingEmail", b =>
                {
                    b.HasOne("DecaBlog.Models.User", "User")
                        .WithOne("SupportingEmails")
                        .HasForeignKey("DecaBlog.Models.SupportingEmail", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DecaBlog.Models.UserComment", b =>
                {
                    b.HasOne("DecaBlog.Models.User", "User")
                        .WithMany("UserComment")
                        .HasForeignKey("CommenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DecaBlog.Models.ArticleTopic", "ArticleTopic")
                        .WithMany("UserComment")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArticleTopic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DecaBlog.Models.UserLike", b =>
                {
                    b.HasOne("DecaBlog.Models.User", "User")
                        .WithOne("UserLike")
                        .HasForeignKey("DecaBlog.Models.UserLike", "LikerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DecaBlog.Models.ArticleTopic", "ArticleTopic")
                        .WithMany("UserLike")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArticleTopic");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DecaBlog.Models.UserSquad", b =>
                {
                    b.HasOne("DecaBlog.Models.Squad", "Squad")
                        .WithMany("UserSquads")
                        .HasForeignKey("SquadId");

                    b.HasOne("DecaBlog.Models.User", "User")
                        .WithMany("UserSquads")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Squad");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DecaBlog.Models.UserStack", b =>
                {
                    b.HasOne("DecaBlog.Models.Stack", "Stack")
                        .WithMany("UserStacks")
                        .HasForeignKey("StackId");

                    b.HasOne("DecaBlog.Models.User", "User")
                        .WithMany("UserStacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stack");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("DecaBlog.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DecaBlog.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DecaBlog.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DecaBlog.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DecaBlog.Models.ArticleTopic", b =>
                {
                    b.Navigation("ArticleList");

                    b.Navigation("UserComment");

                    b.Navigation("UserLike");
                });

            modelBuilder.Entity("DecaBlog.Models.Category", b =>
                {
                    b.Navigation("ArticleTopics");
                });

            modelBuilder.Entity("DecaBlog.Models.Squad", b =>
                {
                    b.Navigation("UserSquads");
                });

            modelBuilder.Entity("DecaBlog.Models.Stack", b =>
                {
                    b.Navigation("UserStacks");
                });

            modelBuilder.Entity("DecaBlog.Models.User", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("Articles");

                    b.Navigation("Photos");

                    b.Navigation("SupportingEmails");

                    b.Navigation("UserComment");

                    b.Navigation("UserLike");

                    b.Navigation("UserSquads");

                    b.Navigation("UserStacks");
                });

            modelBuilder.Entity("DecaBlog.Models.UserComment", b =>
                {
                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
