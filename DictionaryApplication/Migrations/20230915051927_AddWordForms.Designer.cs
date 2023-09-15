﻿// <auto-generated />
using System;
using DictionaryApplication.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DictionaryApplication.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230915051927_AddWordForms")]
    partial class AddWordForms
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Dict")
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DictionaryApplication.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("Users", "Dict");
                });

            modelBuilder.Entity("DictionaryApplication.Models.DbModels.LexemeInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("TranslatedLexemeId")
                        .HasColumnType("int");

                    b.Property<string>("Translation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("TranslatedLexemeId");

                    b.ToTable("LexemeInformations", "Dict");
                });

            modelBuilder.Entity("DictionaryApplication.Models.DbModels.RelatedLexeme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LexemeInformationId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LexemeInformationId");

                    b.ToTable("RelatedLexemes", "Dict");
                });

            modelBuilder.Entity("DictionaryApplication.Models.DbModels.UsageExample", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LexemeInformationId")
                        .HasColumnType("int");

                    b.Property<string>("NativeExample")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TranslatedExample")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LexemeInformationId");

                    b.ToTable("UsageExamples", "Dict");
                });

            modelBuilder.Entity("DictionaryApplication.Models.DbModels.WordForm", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("LexemeId")
                        .HasColumnType("int");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LexemeId");

                    b.ToTable("WordForm", "Dict");
                });

            modelBuilder.Entity("DictionaryApplication.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IsNativeCount")
                        .HasColumnType("int");

                    b.Property<int>("IsStudiedCount")
                        .HasColumnType("int");

                    b.Property<string>("LangCode")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Location")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Languages", "Dict");
                });

            modelBuilder.Entity("DictionaryApplication.Models.Lexeme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CorrectTestAttempts")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("DictionaryId")
                        .HasColumnType("int");

                    b.Property<int>("LangId")
                        .HasColumnType("int");

                    b.Property<int>("TotalTestAttempts")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("Transcription")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Word")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("DictionaryId");

                    b.HasIndex("LangId");

                    b.ToTable("Lexemes", "Dict");
                });

            modelBuilder.Entity("DictionaryApplication.Models.LexemeTranslationPair", b =>
                {
                    b.Property<int>("LexemeId")
                        .HasColumnType("int");

                    b.Property<int>("TranslationId")
                        .HasColumnType("int");

                    b.HasKey("LexemeId", "TranslationId");

                    b.HasIndex("TranslationId");

                    b.HasIndex("LexemeId", "TranslationId")
                        .IsUnique();

                    b.ToTable("LexemeTranslationPairs", "Dict", t =>
                        {
                            t.HasCheckConstraint("CHK_Dictionary_Languages_Not_Equal", "LexemeId <> TranslationId");
                        });
                });

            modelBuilder.Entity("DictionaryApplication.Models.UserDictionary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StudiedLangId")
                        .HasColumnType("int");

                    b.Property<int>("TranslationLangId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TranslationLangId");

                    b.HasIndex("UserId");

                    b.HasIndex("StudiedLangId", "TranslationLangId");

                    b.ToTable("UserDictionaries", "Dict", t =>
                        {
                            t.HasCheckConstraint("CHK_Dictionary_Languages_Not_Equal", "StudiedLangId <> TranslationLangId")
                                .HasName("CHK_Dictionary_Languages_Not_Equal1");
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", "Dict");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", "Dict");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", "Dict");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", "Dict");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", "Dict");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", "Dict");
                });

            modelBuilder.Entity("DictionaryApplication.Models.DbModels.LexemeInformation", b =>
                {
                    b.HasOne("DictionaryApplication.Models.Lexeme", "TranslatedLexeme")
                        .WithMany("LexemeInformations")
                        .HasForeignKey("TranslatedLexemeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TranslatedLexeme");
                });

            modelBuilder.Entity("DictionaryApplication.Models.DbModels.RelatedLexeme", b =>
                {
                    b.HasOne("DictionaryApplication.Models.DbModels.LexemeInformation", "LexemeInformation")
                        .WithMany("RelatedLexemes")
                        .HasForeignKey("LexemeInformationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("LexemeInformation");
                });

            modelBuilder.Entity("DictionaryApplication.Models.DbModels.UsageExample", b =>
                {
                    b.HasOne("DictionaryApplication.Models.DbModels.LexemeInformation", "LexemeInformation")
                        .WithMany("Examples")
                        .HasForeignKey("LexemeInformationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("LexemeInformation");
                });

            modelBuilder.Entity("DictionaryApplication.Models.DbModels.WordForm", b =>
                {
                    b.HasOne("DictionaryApplication.Models.Lexeme", "Lexeme")
                        .WithMany("WordForms")
                        .HasForeignKey("LexemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lexeme");
                });

            modelBuilder.Entity("DictionaryApplication.Models.Lexeme", b =>
                {
                    b.HasOne("DictionaryApplication.Models.UserDictionary", "Dictionary")
                        .WithMany("Lexemes")
                        .HasForeignKey("DictionaryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DictionaryApplication.Models.Language", "LexemeLanguage")
                        .WithMany("Lexemes")
                        .HasForeignKey("LangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Dictionary");

                    b.Navigation("LexemeLanguage");
                });

            modelBuilder.Entity("DictionaryApplication.Models.LexemeTranslationPair", b =>
                {
                    b.HasOne("DictionaryApplication.Models.Lexeme", "Lexeme")
                        .WithMany("LexemePairs")
                        .HasForeignKey("LexemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DictionaryApplication.Models.Lexeme", "Translation")
                        .WithMany("TranslationPairs")
                        .HasForeignKey("TranslationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Lexeme");

                    b.Navigation("Translation");
                });

            modelBuilder.Entity("DictionaryApplication.Models.UserDictionary", b =>
                {
                    b.HasOne("DictionaryApplication.Models.Language", "StudiedLanguage")
                        .WithMany("StudiedUserDictionaries")
                        .HasForeignKey("StudiedLangId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DictionaryApplication.Models.Language", "TranslationLanguage")
                        .WithMany("TranslationUserDictionaries")
                        .HasForeignKey("TranslationLangId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DictionaryApplication.Models.ApplicationUser", "User")
                        .WithMany("UserDictionaries")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudiedLanguage");

                    b.Navigation("TranslationLanguage");

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
                    b.HasOne("DictionaryApplication.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DictionaryApplication.Models.ApplicationUser", null)
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

                    b.HasOne("DictionaryApplication.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DictionaryApplication.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DictionaryApplication.Models.ApplicationUser", b =>
                {
                    b.Navigation("UserDictionaries");
                });

            modelBuilder.Entity("DictionaryApplication.Models.DbModels.LexemeInformation", b =>
                {
                    b.Navigation("Examples");

                    b.Navigation("RelatedLexemes");
                });

            modelBuilder.Entity("DictionaryApplication.Models.Language", b =>
                {
                    b.Navigation("Lexemes");

                    b.Navigation("StudiedUserDictionaries");

                    b.Navigation("TranslationUserDictionaries");
                });

            modelBuilder.Entity("DictionaryApplication.Models.Lexeme", b =>
                {
                    b.Navigation("LexemeInformations");

                    b.Navigation("LexemePairs");

                    b.Navigation("TranslationPairs");

                    b.Navigation("WordForms");
                });

            modelBuilder.Entity("DictionaryApplication.Models.UserDictionary", b =>
                {
                    b.Navigation("Lexemes");
                });
#pragma warning restore 612, 618
        }
    }
}
