﻿#region Copyright
// /************************************************************************
//    Copyright (c) 2016 Jamie Rees
//    File: PlexAvailabilityCheckerTests.cs
//    Created By: Jamie Rees
//   
//    Permission is hereby granted, free of charge, to any person obtaining
//    a copy of this software and associated documentation files (the
//    "Software"), to deal in the Software without restriction, including
//    without limitation the rights to use, copy, modify, merge, publish,
//    distribute, sublicense, and/or sell copies of the Software, and to
//    permit persons to whom the Software is furnished to do so, subject to
//    the following conditions:
//   
//    The above copyright notice and this permission notice shall be
//    included in all copies or substantial portions of the Software.
//   
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//    NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//    OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ************************************************************************/
#endregion
using System;
using System.Collections.Generic;

using Moq;

using NUnit.Framework;

using PlexRequests.Api.Interfaces;
using PlexRequests.Api.Models.Plex;
using PlexRequests.Core;
using PlexRequests.Core.SettingModels;
using PlexRequests.Helpers.Exceptions;
using PlexRequests.Services.Interfaces;
using PlexRequests.Store;
using PlexRequests.Helpers;

namespace PlexRequests.Services.Tests
{
    [TestFixture]
    public class PlexAvailabilityCheckerTests
    {
        public IAvailabilityChecker Checker { get; set; }

        //[Test]
        //public void IsAvailableWithEmptySettingsTest()
        //{
        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();
        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    Assert.Throws<ApplicationSettingsException>(() => Checker.IsAvailable("title", "2013", null, PlexType.TvShow), "We should be throwing an exception since we cannot talk to the services.");
        //}

        //[Test]
        //public void IsAvailableTest()
        //{
        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    var searchResult = new PlexSearch { Video = new List<Video> { new Video { Title = "title", Year = "2011" } } };

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "abc" });
        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    plexMock.Setup(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>())).Returns(searchResult);

        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    var result = Checker.IsAvailable("title", "2011", null, PlexType.Movie);

        //    Assert.That(result, Is.True);
        //}

        //[Test]
        //public void IsAvailableMusicDirectoryTitleTest()
        //{
        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    var searchResult = new PlexSearch { Directory = new List<Directory1> { new Directory1 { Title = "title", Year = "2013", ParentTitle = "dIzZy"} } };

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "abc" });
        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    plexMock.Setup(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>())).Returns(searchResult);

        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    var result = Checker.IsAvailable("title", "2013", "dIzZy", PlexType.Music);

        //    Assert.That(result, Is.True);
        //}

        //[Test]
        //public void IsNotAvailableMusicDirectoryTitleTest()
        //{
        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    var searchResult = new PlexSearch { Directory = new List<Directory1> { new Directory1 { Title = "titale2", Year = "1992", ParentTitle = "dIzZy" } } };

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "abc" });
        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    plexMock.Setup(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>())).Returns(searchResult);

        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    var result = Checker.IsAvailable("title", "2013", "dIzZy", PlexType.Music);

        //    Assert.That(result, Is.False);
        //}

        //[Test]
        //public void IsAvailableDirectoryTitleWithoutYearTest()
        //{
        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    var searchResult = new PlexSearch { Directory = new List<Directory1> { new Directory1 { Title = "title", } } };

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "abc" });
        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    plexMock.Setup(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>())).Returns(searchResult);

        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    var result = Checker.IsAvailable("title", null, null, PlexType.Movie);

        //    Assert.That(result, Is.True);
        //}

        //[Test]
        //public void IsNotAvailableTest()
        //{
        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    var searchResult = new PlexSearch { Video = new List<Video> { new Video { Title = "wrong title", Year = "2011" } } };

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "abc" });
        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    plexMock.Setup(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>())).Returns(searchResult);

        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    var result = Checker.IsAvailable("title", "2011", null, PlexType.Movie);

        //    Assert.That(result, Is.False);
        //}

        //[Test]
        //public void IsNotAvailableTestWihtoutYear()
        //{
        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    var searchResult = new PlexSearch { Video = new List<Video> { new Video { Title = "wrong title" } } };

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "abc" });
        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    plexMock.Setup(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>())).Returns(searchResult);

        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    var result = Checker.IsAvailable("title", null, null, PlexType.Movie);

        //    Assert.That(result, Is.False);
        //}

        //[Test]
        //public void IsYearDoesNotMatchTest()
        //{
        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    var searchResult = new PlexSearch { Video = new List<Video> { new Video { Title = "title", Year = "2019" } } };

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "abc" });
        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    plexMock.Setup(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>())).Returns(searchResult);

        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    var result = Checker.IsAvailable("title", "2011", null, PlexType.Movie);

        //    Assert.That(result, Is.False);
        //}

        //[Test]
        //public void TitleDoesNotMatchTest()
        //{
        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    var searchResult = new PlexSearch { Video = new List<Video> { new Video { Title = "title23", Year = "2019" } } };

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "abc" });
        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    plexMock.Setup(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>())).Returns(searchResult);

        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    var result = Checker.IsAvailable("title", "2019", null, PlexType.Movie);

        //    Assert.That(result, Is.False);
        //}

        //[Test]
        //public void TitleDoesNotMatchWithoutYearTest()
        //{
        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    var searchResult = new PlexSearch { Video = new List<Video> { new Video { Title = "title23" } } };

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "abc" });
        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    plexMock.Setup(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>())).Returns(searchResult);

        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    var result = Checker.IsAvailable("title", null, null, PlexType.Movie);

        //    Assert.That(result, Is.False);
        //}


        //[Test]
        //public void CheckAndUpdateNoPlexSettingsTest()
        //{
        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    Checker.CheckAndUpdateAll(1);

        //    requestMock.Verify(x => x.BatchUpdate(It.IsAny<List<RequestedModel>>()), Times.Never);
        //    requestMock.Verify(x => x.Get(It.IsAny<int>()), Times.Never);
        //    plexMock.Verify(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>()), Times.Never);
        //}

        //[Test]
        //public void CheckAndUpdateNoAuthSettingsTest()
        //{
        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "123" });

        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    Checker.CheckAndUpdateAll(1);

        //    requestMock.Verify(x => x.BatchUpdate(It.IsAny<List<RequestedModel>>()), Times.Never);
        //    requestMock.Verify(x => x.Get(It.IsAny<int>()), Times.Never);
        //    plexMock.Verify(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>()), Times.Never);
        //}

        //[Test]
        //public void CheckAndUpdateNoRequestsTest()
        //{
        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "192.168.1.1" });
        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    requestMock.Setup(x => x.GetAll()).Returns(new List<RequestedModel>());

        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    Checker.CheckAndUpdateAll(1);

        //    requestMock.Verify(x => x.BatchUpdate(It.IsAny<List<RequestedModel>>()), Times.Never);
        //    requestMock.Verify(x => x.Get(It.IsAny<int>()), Times.Never);
        //    plexMock.Verify(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>()), Times.Never);
        //}


        //[Test]
        //public void CheckAndUpdateRequestsThatDoNotExistInPlexTest()
        //{

        //    var requests = new List<RequestedModel> {
        //        new RequestedModel
        //        {
        //            Id = 123,
        //            Title = "title1",
        //            Available = false,
        //        },
        //        new RequestedModel
        //        {
        //            Id=222,
        //            Title = "title3",
        //            Available = false
        //        },
        //        new RequestedModel
        //        {
        //            Id = 333,
        //            Title= "missingTitle",
        //            Available = false
        //        },
        //        new RequestedModel
        //        {
        //            Id= 444,
        //            Title = "already found",
        //            Available = true
        //        }
        //    };

        //    var search = new PlexSearch
        //    {
        //        Video = new List<Video>
        //        {
        //            new Video
        //            {
        //                Title = "Title4",
        //                Year = "2012"
        //            },
        //            new Video
        //            {
        //                Title = "Title2",
        //            }
        //        },
        //        Directory = new List<Directory1> { new Directory1
        //        {
        //            Title = "Title9",
        //            Year = "1978"
        //        }}
        //    };

        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "192.168.1.1" });
        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    requestMock.Setup(x => x.GetAll()).Returns(requests);
        //    plexMock.Setup(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>())).Returns(search);
        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    Checker.CheckAndUpdateAll(1);

        //    requestMock.Verify(x => x.BatchUpdate(It.IsAny<List<RequestedModel>>()), Times.Never);
        //    requestMock.Verify(x => x.Get(It.IsAny<int>()), Times.Never);
        //    plexMock.Verify(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>()), Times.Exactly(3));
        //}

        //[Test]
        //public void CheckAndUpdateRequestsAllRequestsTest()
        //{

        //    var requests = new List<RequestedModel> {
        //        new RequestedModel
        //        {
        //            Id = 123,
        //            Title = "title1",
        //            Available = false,
        //        },
        //        new RequestedModel
        //        {
        //            Id=222,
        //            Title = "title3",
        //            Available = false
        //        },
        //        new RequestedModel
        //        {
        //            Id = 333,
        //            Title= "missingTitle",
        //            Available = false
        //        },
        //        new RequestedModel
        //        {
        //            Id= 444,
        //            Title = "Hi",
        //            Available = false
        //        }
        //    };

        //    var search = new PlexSearch
        //    {
        //        Video = new List<Video>
        //        {
        //            new Video
        //            {
        //                Title = "title1",
        //                Year = "2012"
        //            },
        //            new Video
        //            {
        //                Title = "Title3",
        //            }
        //            ,
        //            new Video
        //            {
        //                Title = "Hi",
        //            }
        //        },
        //        Directory = new List<Directory1> { new Directory1
        //        {
        //            Title = "missingTitle",
        //            Year = "1978"
        //        }}
        //    };

        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "192.168.1.1" });
        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    requestMock.Setup(x => x.GetAll()).Returns(requests);
        //    plexMock.Setup(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>())).Returns(search);
        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    Checker.CheckAndUpdateAll(1);

        //    requestMock.Verify(x => x.BatchUpdate(It.IsAny<List<RequestedModel>>()), Times.Once);
        //    requestMock.Verify(x => x.Get(It.IsAny<int>()), Times.Never);
        //    plexMock.Verify(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>()), Times.Exactly(4));
        //}


        //[Test]
        //public void CheckAndUpdateAllMusicRequestsTest()
        //{

        //    var requests = new List<RequestedModel> {
        //        new RequestedModel
        //        {
        //            Id = 123,
        //            Title = "title1",
        //            Available = false,
        //            ArtistName = "dizzy",
        //            Type = RequestType.Album,
        //            ReleaseDate = new DateTime(2010,1,1)
        //        },
        //        new RequestedModel
        //        {
        //            Id=222,
        //            Title = "title3",
        //            Available = false,
        //            ArtistName = "a",
        //            Type = RequestType.Album,
        //            ReleaseDate = new DateTime(2006,1,1)
        //        },
        //        new RequestedModel
        //        {
        //            Id = 333,
        //            Title= "missingTitle",
        //            Available = false,
        //            ArtistName = "b",
        //            Type = RequestType.Album,
        //            ReleaseDate = new DateTime(1992,1,1)
        //        },
        //        new RequestedModel
        //        {
        //            Id= 444,
        //            Title = "Hi",
        //            Available = false,
        //            ArtistName = "c",
        //            Type = RequestType.Album,
        //            ReleaseDate = new DateTime(2017,1,1)
        //        }
        //    };

        //    var search = new PlexSearch
        //    {
        //        Directory = new List<Directory1> {
        //            new Directory1
        //            {
        //                Title = "missingTitle",
        //                Year = "1978",
        //                ParentTitle = "c"
        //            },
        //            new Directory1
        //            {
        //                Title = "Hi",
        //                Year = "1978",
        //                ParentTitle = "c"
        //            },
        //            new Directory1
        //            {
        //                Title = "Hi",
        //                Year = "2017",
        //                ParentTitle = "c"
        //            },
        //            new Directory1
        //            {
        //                Title = "missingTitle",
        //                Year = "1992",
        //                ParentTitle = "b"
        //            },
        //            new Directory1
        //            {
        //                Title = "title1",
        //                Year = "2010",
        //                ParentTitle = "DiZzY"
        //            },
        //        }
        //    };

        //    var settingsMock = new Mock<ISettingsService<PlexSettings>>();
        //    var authMock = new Mock<ISettingsService<AuthenticationSettings>>();
        //    var requestMock = new Mock<IRequestService>();
        //    var plexMock = new Mock<IPlexApi>();
        //    var cacheMock = new Mock<ICacheProvider>();

        //    settingsMock.Setup(x => x.GetSettings()).Returns(new PlexSettings { Ip = "192.168.1.1" });
        //    authMock.Setup(x => x.GetSettings()).Returns(new AuthenticationSettings { PlexAuthToken = "abc" });
        //    requestMock.Setup(x => x.GetAll()).Returns(requests);
        //    plexMock.Setup(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>())).Returns(search);
        //    Checker = new PlexAvailabilityChecker(settingsMock.Object, authMock.Object, requestMock.Object, plexMock.Object, cacheMock.Object);

        //    Checker.CheckAndUpdateAll(1);

        //    requestMock.Verify(x => x.BatchUpdate(It.IsAny<List<RequestedModel>>()), Times.Once);
        //    requestMock.Verify(x => x.Get(It.IsAny<int>()), Times.Never);
        //    plexMock.Verify(x => x.SearchContent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Uri>()), Times.Exactly(4));
        //}
    }
}