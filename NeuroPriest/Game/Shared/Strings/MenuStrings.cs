namespace NeuroPriest.Shared
{
    internal static class MenuStrings
    {
        public const string GAMEPLAY =
            @"
This is a turn based game
You die once you get hit by anything

   (^)
(<)(v)(>)  @  Movement

Move into enemies to attack
Can only attack in certain directions
Killing increses Sin by one
At full Sin, you cannot move
To reset Sin, you must commit Penance

( Ctrl )   @  Penance

Underneath Sin, Modifiers will appear

Each map is seperated into rooms.
Once you enter a room, enemies will awaken
Kill all the enemies to clear the level

You say a prayer everytime you enter a room
Enemies CANNOT step (but can kill) on entrances
Use this to your advantage
";

        public const string ENEMIES =
            @"
ENEMIES

A  @  Basic factory Android
I  @  Interfacer, old factory droids
>  @  War era Observers, Type A
v  @  War era Observers, Type B
S  @  Seekers used for counterintelligence
";

        public const string INTERACTABLES =
            @"
INTERACTABLES

O  @  Crate. Breakable
X  @  Heat Vents
=  @  Containment mine
L  @  Lever. Opens confinement doors
7  @  Confinement doors
";

        public const string CREDITS =
            @"
Thanks to IcarusLives for getting me into programming

Base Font      @  Born2bSportyV2             @  JapanYoshi
Sound Effects  @  Youtube and Sample Focus   @  Many
area1.mp3      @  Ждите нас звезды!          @  Project Lazarus
area2.mp3      @  Мгновенья мимолетный цвет  @  Project Lazarus
area3.mp3      @  Здравствуйте               @  Project Lazarus
title.mp3      @  Ri9                        @  Project Lazarus
end.mp3        @  Furthest Light             @  Ruto
";

        public const string WIN =
            @"
 {[[[[[[[[[[[[@@[[[[[[[[[[[[\
:|                          ]/
 £                          £ 
 }   VINCIT OMNIA VERITAS   `
 £                          £
:|                          ]/
 ""[[[[[[[[[[[[@@[[[[[[[[[[[[~

 @ Enemies Defeated.";

        public const string LOSE =
            @"
 {[@[[[[[[[[[[==[[[[[[[[[[@[\
:|                          ]/
 =                          = 
 }      MORIOR INVICTUS     `
 =                          =
:|                          ]/
 ""[@[[[[[[[[[[==[[[[[[[[[[@[~

 @ Killed in battle.";
    }
}
