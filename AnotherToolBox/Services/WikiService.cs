using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AnotherToolBox.Models.Characters;
using AnotherToolBox.Models.Team;
using Avalonia.Media;
using Microsoft.Extensions.Logging;
using WikiClientLibrary;
using WikiClientLibrary.Cargo.Linq;
using WikiClientLibrary.Cargo.Schema;
using WikiClientLibrary.Cargo.Schema.DataAnnotations;
using WikiClientLibrary.Client;
using WikiClientLibrary.Infrastructures;
using WikiClientLibrary.Pages.Parsing;
using WikiClientLibrary.Sites;

namespace AnotherToolBox.Services;

public class WikiService
{
    private readonly ILogger<WikiService> _logger;
    const string ApiEndpoint = "https://anothereden.wiki/api.php";
    const string UserAgent = "AnotherToolbox/1.0 (Hovrawl)";
    const string CacheDir = "cache";
    
    private WikiClient _client;
    private WikiSite _site;
    public List<CharacterSlim> SlimCharacters = new();
    public List<WeaponCargo> CargoWeapons = new();
    public List<ArmorCargo> CargoArmor = new();
    public List<GrastaCargo> CargoGrasta = new();
    public List<BadgeCargo> CargoBadges = new();
    private Dictionary<string, CargoSkill> _characterSkills = new();

    public bool Initialized { get; private set; }
    
    public WikiService(ILogger<WikiService> logger)
    {
        _logger = logger;
    }

    
    public async Task Setup()
    {
        
        // A WikiClient has its own CookieContainer.
        var client = new WikiClient()
        {
            ClientUserAgent = UserAgent,
            Logger = _logger,
        };
        
        // You can create multiple WikiSite instances on the same WikiClient to share the state.
        var site = new WikiSite(client, ApiEndpoint)
        {
            Logger = _logger
        };
        
        // Wait for initialization to complete.
        // Throws error if any.
        await site.Initialization;

        // Successful init
        
        _client = client;
        _site = site;
        Initialized = true;
        
        // var cancellationToken = CancellationToken.None;
        // var fields = new
        // {
        //     action = "query",
        //     prop = "revisions",
        //     rvprop = "content",
        //     titles = "aisha"
        // };
        // var request = new MediaWikiFormRequestMessage(fields);
        //
        // // site.ParsePageAsync(ApiEndpoint, ParsingOptions.DisableLimitReport);
        // // try
        // // {
        // //     // await site.LoginAsync("User name", "password");
        // //     var results =site.InvokeMediaWikiApiAsync(request, cancellationToken);
        // //     // var parser = new MediaWikiJsonResponseParser();
        // //     // var results = await site.InvokeMediaWikiApiAsync(request, parser, true, cancellationToken);
        // // }
        // // catch (WikiClientException ex)
        // // {
        // //     Console.WriteLine(ex.Message);
        // //     // Add your exception handler for failed login attempt.
        // // }

       
    }

    public async Task<bool> Shutdown()
    {
        await _site.LogoutAsync();
        _client.Dispose();

        Initialized = false;
        return true;
    }
    
    public async Task LoadCharactersSlim()
    {
        SlimCharacters.Clear();
        try
        {
            var context = new CargoQueryContext(_site) { PaginationSize = 500 };
            var query = context.Table<CharacterSlim>()
                .Where(c => c.ReleaseDate != null)
                .OrderByDescending(c => c.ReleaseDate)
                .Take(9999);
            await foreach (var ch in query.AsAsyncEnumerable())
            {
                // Use ch.Name, ch.Element, etc.  
                SlimCharacters.Add(ch);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Add your exception handler for failed login attempt.
        }
    }


    public async Task<List<string>> FetchCharacterThumbnails(List<CharacterChoiceDto> characterSlims)
    {
        var cts = new CancellationTokenSource();
        var tasks = characterSlims.Select(async c =>  
        {  
            var filename = BuildCharacterImageFilename(c.Id, style: c.Style, maxRarity: 5);  
            // -> "101910011_rank5 command.png"  
              
            // Resolve to URL  
            var info = await _site.InvokeMediaWikiApiAsync(new MediaWikiFormRequestMessage(new  
            {  
                action = "query",  
                titles = $"File:{filename}",  
                prop = "imageinfo",  
                iiprop = "url"  
            }), cts.Token);  
            var url = info["query"]["pages"][0]["imageinfo"][0]["url"].GetValue<string>();
            return url;  
        });  
        var urls = await Task.WhenAll(tasks);
        
        return urls.ToList();
    }
    
    static string BuildCharacterImageFilename(string id, int? style, int? maxRarity)  
    {  
        var suffix = style switch  
        {  
            2 or 3 or 4 => $"_s{style}",  
            _ => ""  
        };  
        var rank5 = maxRarity == 5 ? "_rank5" : "";  
        return $"{id}{suffix}{rank5} command.png";  
    }

    public async Task LoadWeapons()
    {
        CargoWeapons.Clear();
        try
        {
            var context = new CargoQueryContext(_site) { PaginationSize = 500 };
            var query = context.Table<WeaponCargo>()
                .Where(c => !c.Unreleased)
                .OrderByDescending(c => c.Level)
                .Take(9999);
            await foreach (var ch in query.AsAsyncEnumerable())
            {
                // Use ch.Name, ch.Element, etc.  
                CargoWeapons.Add(ch);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Add your exception handler for failed login attempt.
        }
    }
    
    public async Task LoadArmors()
    {
        CargoArmor.Clear();
        try
        {
            var context = new CargoQueryContext(_site) { PaginationSize = 500 };
            var query = context.Table<ArmorCargo>()
                .Where(c => !c.Unreleased)
                .OrderByDescending(c => c.Level)
                .Take(9999);
            await foreach (var ch in query.AsAsyncEnumerable())
            {
                // Use ch.Name, ch.Element, etc.  
                CargoArmor.Add(ch);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    public async Task LoadGrasta()
    {
        CargoGrasta.Clear();
        try
        {
            var context = new CargoQueryContext(_site) { PaginationSize = 500 };
            var query = context.Table<GrastaCargo>()
                .Where(c => !c.Unreleased)
                .OrderByDescending(c => c.Tier)
                .Take(9999);
            await foreach (var ch in query.AsAsyncEnumerable())
            {
                // Use ch.Name, ch.Element, etc.  
                CargoGrasta.Add(ch);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    public async Task LoadBadges()
    {
        CargoBadges.Clear();
        try
        {
            var context = new CargoQueryContext(_site) { PaginationSize = 500 };
            var query = context.Table<BadgeCargo>()
                .Where(c => !c.Unreleased)
                .Take(9999);
            await foreach (var ch in query.AsAsyncEnumerable())
            {
                // Use ch.Name, ch.Element, etc.  
                CargoBadges.Add(ch);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    
    public async Task<CharacterStatsCargo> LoadCharacterStats(string pageId)
    {
        try
        {
            var context = new CargoQueryContext(_site) { PaginationSize = 10 };
            var query = context
                .Table<CharacterStatsCargo>()
                .Where(c => c.PageId == pageId).Take(1);
            await foreach (var row in query.AsAsyncEnumerable())
            {
                return row; // first row
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return null;
    }

    private Dictionary<string, CargoSkill> dupedSkills = new();
    
    private async Task CacheCharacterSkills()
    {
        var totalRows = 0;
        try
        {

            // Todo - not all skills are found uniquely through page name
            var context = new CargoQueryContext(_site) { PaginationSize = 500 };
            var query = context
                .Table<CargoSkill>()
                .Where(c => c.PageName != null).Take(9999);
            await foreach (var row in query.AsAsyncEnumerable())
            {
                totalRows++;
                if (_characterSkills.ContainsKey(row.PageName))
                {
                    var newName = $"{row.PageName}__{totalRows}";
                    dupedSkills[newName] = row;
                    continue;
                }
                _characterSkills[row.PageName] = row; // first row
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        // Console.WriteLine($"Fetched rows: {totalRows}");
        // Console.WriteLine($"Unique keys: {_characterSkills.Count}");
    }
    
    public async Task<ICollection<CargoSkill>> LoadCharacterSkills(CharacterSlim character)
    {
        var returnCollection = new List<CargoSkill>();
        try
        {
            var context = new CargoQueryContext(_site) { PaginationSize = 10 };
            var query = context
                .Table<CharacterDetailsDto>()
                .Where(c => c.Id == character.Id).Take(1);
            CharacterDetailsDto? characterDetails = null;
            await foreach (var row in query.AsAsyncEnumerable())
            {
                characterDetails = row; // first row
            }
            
            if (characterDetails == null) return returnCollection;

            // Cache skills
            if (_characterSkills.Count < 1)
            {
                await CacheCharacterSkills();
            }
            
            // filter our loaded skills
            foreach (var skillName in characterDetails.Skills)
            {
                if (_characterSkills.TryGetValue(skillName, out var skill))
                {
                    returnCollection.Add(skill);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return returnCollection;
    }
}