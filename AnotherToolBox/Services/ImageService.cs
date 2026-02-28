using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media.Imaging;

namespace AnotherToolBox.Services;

public class ImageService : IDisposable
{
    private static readonly HttpClient _httpClient = new()
    {
        Timeout = TimeSpan.FromSeconds(30)
    };

    // Simple in-memory cache. Consider an LRU or disk-based cache if you have many images.
    private readonly ConcurrentDictionary<string, Bitmap> _cache = new();
    // Maximum allowed size (e.g., 5MB)
    private const long MaxBytes = 5 * 1024 * 1024;

    public Task<Bitmap?> GetBitmapFromUrlAsync(string url, CancellationToken cancellationToken = default)
        => GetBitmapFromUrlInternalAsync(url, cancellationToken);

    private async Task<Bitmap?> GetBitmapFromUrlInternalAsync(string url, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(url))
            return null;

        if (_cache.TryGetValue(url, out var cached))
            return cached;

        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            using var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            // Optional: protect against very large responses
            var contentLength = response.Content.Headers.ContentLength;
            if (contentLength.HasValue && contentLength.Value > MaxBytes)
                return null;

            await using var responseStream = await response.Content.ReadAsStreamAsync(cancellationToken).ConfigureAwait(false);
            // Copy to memory stream so we can set position etc.
            await using var ms = new MemoryStream();
            await responseStream.CopyToAsync(ms, cancellationToken).ConfigureAwait(false);
            ms.Position = 0;

            // Construct Bitmap from the memory stream.
            // This constructor will decode the image.
            var bitmap = new Bitmap(ms);

            // Cache the bitmap for reuse.
            _cache.TryAdd(url, bitmap);

            return bitmap;
        }
        catch (OperationCanceledException) { return null; }
        catch (Exception)
        {
            // Log if you have logging; swallow to avoid crashing UI.
            return null;
        }
    }

    public void Dispose()
    {
        foreach (var kv in _cache.Values)
        {
            kv.Dispose();
        }
        _cache.Clear();
    }
}