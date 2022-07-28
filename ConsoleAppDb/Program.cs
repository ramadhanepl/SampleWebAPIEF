// See https://aka.ms/new-console-template for more information
using ConsoleAppDb;
using Microsoft.EntityFrameworkCore;
using SampleWebAPI.Data;
using SampleWebAPI.Domain;


//Entity Framework
SamuraiContext _context = new SamuraiContext();
_context.Database.EnsureCreated();

//Console.WriteLine("Sebelum tambah data samurai");
//GetById(9);
//Console.WriteLine("Tambah data samurai");
//AddSamurai();
//AddMultipleSamurai("samurai1", "samurai2", "samurai3", "samurai4", "samurai5", "samurai6");
//GetSamuraiByName("Gyu");
//var data = GetById(2);
//Console.WriteLine($"GetById - {data.Id} - {data.Name}");

/*var samurai = _context.Samurais.Find(2);
Console.WriteLine($"{samurai.Id} - {samurai.Name}");*/
//AddMoreThanOneType();

//UpdateSamurai(6,"Rengoku Kyojiro");
//DeleteSamurai(12);
//DeleteMultipleSamurai("Nanao");
//AddSamuraiWithQuote();
//AddQuote();
//AddQuoteToExistingSamurai();
//AddSamuraiToExistingBattle();
//AddSamuraiWithHorse();
//AddHorseToExistingSamurai();
//GetSamurai();
//AddSword();
//AddSwordToExistingSamurai();
//DeleteSword(6);
//GetSamuraiWithSword();
//AddElement("Fire", "Water", "Earth", "Lightning", "Mist", "Sound", "Poison", "Beast");
//GetElement();
//UpdateSword(9, 1.5);
//GetSword();
//AddElementToExistingSword();
GetSwordWithElement();
//AddSwordStyleToExistingSword();
//GetSwordWithSwordStyle();
//GetQuotes();
//GetBattle();
//RemoveSamuraiFromBattle();
//GetBattlesWithSamurais();
//GetQuotesWithSamurai();
//GetSamuraiWithQuotes();
//ProjectionSample();
//GetSamuraiWithHorse();
//QueryWithRawSQLInterpolated();
//GetSamuraiBattleStats();
//QueryUsingSP();


//Console.ReadKey();

// Samurai Query
void AddSamurai()
{
    var samurai = new Samurai { Name = "Rengoku Kyojuro" };
    _context.Samurais.Add(samurai);
    _context.SaveChanges();
}
void AddMultipleSamurai(params string[] names)
{
    foreach(string name in names)
    {
        _context.Samurais.Add(new Samurai { Name= name });
    }
    _context.SaveChanges();
}
void GetSamurai()
{
    var samurais = _context.Samurais.OrderByDescending(s => s.Name).ToList();
    /*var samurais = (from s in _context.Samurais
                   orderby s.Name descending
                   select s).ToList();*/
    Console.WriteLine($"Jumlah samurai: {samurais.Count}");
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Id} - {samurai.Name}");
    }
}
Samurai GetById(int id)
{
    var result = _context.Samurais.Where(s => s.Id == id).FirstOrDefault();
    /*var result = (from s in _context.Samurais
                  where s.Id == id
                  select s).FirstOrDefault();*/
    if (result != null)
    {
        Console.WriteLine($"{result.Id} - {result.Name}");
        return result;

    }
    else
        throw new Exception($"Data dengan id {id} tidak ditemukan");
}
void GetSamuraiByName(string name)
{
    var samurais = _context.Samurais.Where(s => s.Name.Contains(name.ToLower())).OrderBy(s => s.Name).ToList();
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Id} - {samurai.Name}");
    }
}
void UpdateSamurai(int id,string nama)
{
    var samurai = _context.Samurais.FirstOrDefault(s=>s.Id==id);
    if(samurai!=null)
    {
        samurai.Name = nama;
        _context.SaveChanges();
    }
    else
    {
        Console.WriteLine("Data tidak ditemukan");
    }
}
void DeleteSamurai(int id)
{
    var samurai = _context.Samurais.Find(id);
    if(samurai!=null)
    {
        _context.Samurais.Remove(samurai);
        _context.SaveChanges();
    }
    else
    {
        Console.WriteLine("Data tidak ditemukan");
    }
}
void DeleteMultipleSamurai(string name)
{
    var results = _context.Samurais.Where(s => s.Name.Contains(name.ToLower()))
            .OrderBy(s => s.Name).ToList();
    _context.Samurais.RemoveRange(results);
    Console.WriteLine("Data Berhasil di Hapus");
    _context.SaveChanges();
}

// Sword Query
void AddSword()
{
    var sword = new Sword
    {
        SwordName = "God Lightning",
        SamuraiId = 9
    };
    _context.Swords.Add(sword);
    _context.SaveChanges();
}
void UpdateSword(int id, double weight)
{
    var sword = _context.Swords.FirstOrDefault(s => s.Id == id);
    if (sword!=null)
    {
        sword.Weight = weight;
        _context.SaveChanges();
    }
}
void GetSword()
{
    var swords = _context.Swords.OrderBy(s => s.Id).ToList();
    foreach (var sword in swords)
    {
        Console.WriteLine($"{sword.Id} - {sword.SwordName} - {sword.Weight}");
    }
}
void AddSwordToExistingSamurai()
{
    var samurai = _context.Samurais.Find(13);
    if (samurai != null)
    {
        samurai.Swords.Add(new Sword { SwordName = "Taiyo No Kami" });
        _context.SaveChanges();
    }
    var samurai2 = _context.Samurais.Find(16);
    if (samurai2 != null)
    {
        samurai2.Swords.Add(new Sword { SwordName = "Murasaki No Cho" });
        _context.SaveChanges();
    }
    var samurai3 = _context.Samurais.Find(18);
    if (samurai3 != null)
    {
        samurai3.Swords.Add(new Sword { SwordName = "Mizu No Ikari" });
        _context.SaveChanges();
    }
}
void GetSamuraiWithSword()
{
    var samurais = _context.Samurais.Include(s => s.Swords).ToList();
    foreach (var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Name}");
        foreach (var sword in samurai.Swords)
        {
            Console.WriteLine($"Sword Name : {sword.SwordName}");
        }
    }
}
void DeleteSword(int id)
{
    var sword = _context.Swords.Find(id);
    if (sword!=null)
    {
        _context.Swords.Remove(sword);
        _context.SaveChanges();
    }
    else
    {
        Console.WriteLine("Pedang tidak ditemukan");
    }
}

// Quote Query
void AddQuote()
{
    var newQuote = new Quote
    {
        Text = "Dont fear of death",
        SamuraiId = 1
    };
    _context.Quotes.Add(newQuote);
    _context.SaveChanges();
}
void GetSamuraiWithQuotes()
{
    var samurais = _context.Samurais.Include(s => s.Quotes).ToList();
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Name}");
        foreach(var quote in samurai.Quotes)
        {
            Console.WriteLine($"-----> {quote.Text}");
        }
    }
}
void AddSamuraiWithQuote()
{
    var samurai = new Samurai
    {
        Name = "Sanae",
        Quotes = new List<Quote>
        {
            new Quote { Text = "Think lightly of yourself and deeply word" },
            new Quote { Text = "Do nothing that is no use" }
        }
    };
    _context.Samurais.Add(samurai);
    _context.SaveChanges();
}
void GetQuotes()
{
    var quotes = _context.Quotes.OrderBy(q => q.Text).ToList();
    foreach( var quote in quotes)
    {
        Console.WriteLine($"{quote.Text} - {quote.SamuraiId}");
    }
}
void GetQuotesWithSamurai()
{
    var quotes = _context.Quotes.Include(q=>q.Samurai).OrderBy(q => q.Text).ToList();
    foreach(var quote in quotes)
    {
        Console.WriteLine($"{quote.Text} by {quote.Samurai.Name}");
    }
}
void AddQuoteToExistingSamurai()
{
    var samurai = _context.Samurais.Find(9);
    if(samurai!=null)
    {
        samurai.Quotes.Add(new Quote { Text = "Master a single thing"});
        _context.SaveChanges();
    }
}

// Battle Query
void GetBattle()
{
    var battles = _context.Battles.OrderBy(b => b.Name).ToList();
    foreach(var battle in battles)
    {
        Console.WriteLine($"{battle.BattleId} - {battle.Name}");
    }
}
void AddMoreThanOneType()
{
    _context.AddRange(new Samurai { Name = "Muzan Kibursuji" },
        new Samurai { Name = "Haomaru" },
        new Battle { Name = "Battle of Anegawa"},
        new Battle { Name = "Battle of Kyoto"});
    _context.SaveChanges();
}
void AddSamuraiToExistingBattle()
{
    //var battle = _context.Battles.FirstOrDefault(b => b.BattleId == 1);
    //var samurai = _context.Samurais.FirstOrDefault(s => s.Id == 9);
  
    //var samurai1 = _context.Samurais.Find(13);
    var battle2 = _context.Battles.Find(1);
    var samurai3 = _context.Samurais.Find(12);
    var samurai4 = _context.Samurais.Find(18);

    //  var battle2 = _context.Battles.Find(2);
    
    //battle.Samurais.Add(samurai);
    //samurai1.Battles.Add(battle2);
    samurai3.Battles.Add(battle2);
    samurai4.Battles.Add(battle2);

    _context.SaveChanges();
}
void GetBattlesWithSamurais()
{
    var battles = _context.Battles.Include(b => b.Samurais).ToList();
    foreach(var battle in battles)
    {
        Console.WriteLine($"{battle.BattleId} - {battle.Name} :");
        foreach(var samurai in battle.Samurais)
        {
            Console.WriteLine($"-----> {samurai.Id} - {samurai.Name}");
        }
    }
}
void RemoveSamuraiFromBattle()
{
    var battles = _context.Battles.Include(b => b.Samurais.Where(s => s.Id == 9))
        .FirstOrDefault(b => b.BattleId == 1);
    var samurai = battles.Samurais[0];
    battles.Samurais.Remove(samurai);
    _context.SaveChanges();
}

// Element Query
void AddElement(params string[] elements)
{
    foreach (string element in elements)
    {
        _context.Elements.Add(new Element { ElementType = element });
    }
    _context.SaveChanges();
}
void GetElement()
{
    var elements = _context.Elements.OrderBy(e => e.Id).ToList();
    foreach (var element in elements)
    {
        Console.WriteLine($"{element.Id} - {element.ElementType}");
    }
}
void AddElementToExistingSword()
{
    var sword1 = _context.Swords.Find(7);
    var sword2 = _context.Swords.Find(8);
    var sword4 = _context.Swords.Find(9);

    var element1 = _context.Elements.Find(1);
    var element2 = _context.Elements.Find(7);
    var element3 = _context.Elements.Find(2);

    sword1.Elements.Add(element1);
    sword2.Elements.Add(element2);
    sword4.Elements.Add(element3);

    _context.SaveChanges();

}
void GetSwordWithElement()
{
    var swords = _context.Swords.Include(s => s.Elements).ToList();
    foreach (var sword in swords)
    {
        Console.WriteLine($"{sword.Id} - {sword.SwordName} :");
        Console.WriteLine("Element :");
        foreach (var element in sword.Elements)
        {
            Console.WriteLine($"------> {element.ElementType}");
        }
    }
}

// Sword Style
void AddSwordStyleToExistingSword()
{
    var sword = _context.Swords.FirstOrDefault(s => s.Id == 9 );
    sword.SwordType = new SwordType { Style = "Long Sword" };
    _context.SaveChanges();
}
void GetSwordWithSwordStyle()
{
    var swords = _context.Swords.Include(s => s.SwordType).ToList();
    foreach (var sword in swords)
    {
        if (sword.SwordType != null)
        {
            Console.WriteLine($"{sword.SwordName} - {sword.SwordType.Style}");
        }
    }
}

// Horse Query
void AddSamuraiWithHorse()
{
    var samurai = new Samurai { Name = "Kenshin Himura", Horse = new Horse { Name = "White Tornado" } };
    _context.Samurais.Add(samurai);
    _context.SaveChanges();
}
void AddHorseToExistingSamurai()
{
    var samurai = _context.Samurais.FirstOrDefault(s => s.Id == 9);
    samurai.Horse = new Horse { Name = "Red Tornado" };
    _context.SaveChanges();
}
void GetSamuraiWithHorse()
{
    var samurais = _context.Samurais.Include(s => s.Horse).ToList();
    foreach(var samurai in samurais)
    {
        if(samurai.Horse!=null)
            Console.WriteLine($"{samurai.Name} - {samurai.Horse.Name}");
    }
}

// Mixed Query
void ProjectionSample()
{
    var results = _context.Samurais.Include(s=>s.Quotes).Select(s => new { 
        s.Name,
        JumlahQuotes = s.Quotes.Count
    }).ToList();
    foreach(var item in results)
    {
        Console.WriteLine($"{item.Name} - {item.JumlahQuotes}");
    }
}
void QueryWithRawSQL()
{
    //jangan digunakan karena rawan SQL Injection
    string name = "Zenitsu";
    var samurais = _context.Samurais.FromSqlRaw($"select * from Samurais where Name='{name}' ").ToList();
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Name}");
    }
}
void QueryWithRawSQLInterpolated()
{
    string name = "Zenitsu";
    var samurais = _context.Samurais.FromSqlInterpolated($"select * from Samurais where Name={name}").ToList();
    foreach (var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Name}");
    }
}
void GetSamuraiBattleStats()
{
    var stats = _context.SamuraiBattleStats.OrderBy(s => s.Name).ToList();
    foreach(var stat in stats)
    {
        Console.WriteLine($"{stat.Name} - {stat.NumberOfBattles} - {stat.EarliestBattle}");
    }
}
void QueryUsingSP()
{
    var text = "fear";
    var samurais = _context.Samurais.FromSqlInterpolated($"exec dbo.SamuraisWhoSaidAWord {text}").ToList();
    foreach(var samurai in samurais)
    {
        Console.WriteLine($"{samurai.Id} - {samurai.Name}");
    }
}





