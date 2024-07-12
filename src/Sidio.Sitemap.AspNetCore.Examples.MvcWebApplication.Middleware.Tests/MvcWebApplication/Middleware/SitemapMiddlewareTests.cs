﻿using Microsoft.AspNetCore.Mvc.Testing;

namespace Sidio.Sitemap.AspNetCore.Examples.MvcWebApplication.Middleware.Tests.MvcWebApplication.Middleware;

public sealed class SitemapMiddlewareTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;

    public SitemapMiddlewareTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task SitemapHome_ReturnsSitemap()
    {
        // arrange
        var client = _factory.CreateClient();

        // act
        var response = await client.GetAsync("/sitemap.xml");

        // assert
        response.IsSuccessStatusCode.Should().BeTrue();
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("sitemap");
        content.Should().Contain("custom-url");
        content.Should().NotContainEquivalentOf("privacy");
    }
}