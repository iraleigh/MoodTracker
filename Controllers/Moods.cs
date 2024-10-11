using System.ComponentModel;

using Microsoft.EntityFrameworkCore;

public class Moods
{
    public static IResult GetMoods(MoodDb db)
    {
        var moodEntities = db.Moods.ToList<MoodEntity>();

        var moods = new List<Mood>();

        foreach (var moodEntity in moodEntities)
        {
            var mood = new Mood
            {
                Feeling = moodEntity.Feeling,
                Intensity = moodEntity.Intensity,
                Description = moodEntity.Description,
            };

            moods.Add(mood);
        }
        return Results.Ok(moods);
    }

    public static IResult GetMood(int id, MoodDb db)
    {
        var moodEntity = db.Moods.Single<MoodEntity>(m => m.Id == id);
        var mood = new Mood
        {
            Feeling = moodEntity.Feeling,
            Intensity = moodEntity.Intensity,
            Description = moodEntity.Description
        };

        return Results.Ok(mood);
    }

    public static IResult CreateMood(Mood mood, MoodDb db)
    {
        var moodEntity = new MoodEntity
        {
            Feeling = mood.Feeling,
            Intensity = mood.Intensity,
            Description = mood.Description,
        };

        db.Moods.Add(moodEntity);

        db.SaveChanges();

        return Results.Created($"/moods/{moodEntity.Id}", mood);
    }

    public static IResult UpdateMood(int id, Mood mood, MoodDb db)
    {
        var currentMood = db.Moods.SingleOrDefault(m => m.Id == id);

        if (currentMood is not null)
        {
            currentMood.Feeling = mood.Feeling;
            currentMood.Intensity = mood.Intensity;
            currentMood.Description = mood.Description;

            db.SaveChanges();

            return Results.NoContent();
        }

        return Results.NotFound();
    }

    public static IResult DeleteMood(int id, MoodDb db)
    {
        var currentMood = db.Moods.SingleOrDefault(m => m.Id == id);

        if (currentMood is not null)
        {
            db.Moods.Remove(currentMood);
            db.SaveChanges();
            return Results.NoContent();
        }

        return Results.NotFound();
    }
}