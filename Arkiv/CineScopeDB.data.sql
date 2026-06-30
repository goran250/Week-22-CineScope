SET IDENTITY_INSERT [dbo].[Actors] ON
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (1, N'Lasse Åberg', N'Sverige', N'lasse-åberg.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (2, N'Jon Skolmen', N'Sverige', N'jon-skolmen.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (3, N'Kim Anderzon', N'Sverige', N'kim-anderzon.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (4, N'Lottie Ejebrant', N'Sverige', N'lottie-ejebrant.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (5, N'Sven Melander', N'Sverige', N'sven-melander.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (6, N'Weiron Holmberg', N'Sverige', N'weiron-holmberg.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (7, N'Magnus Härenstam', N'Sverige', N'magnus-harenstam.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (8, N'Roland Jansson', N'Sverige', N'roland-jansson.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (9, N'Cecilia Wallton', N'Sverige', N'cecilia-wallton.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (10, N'Eva Millberg', N'Sverige', N'eva-millberg.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (11, N'Bengt Andersson', N'Sverige', N'bengt-andersson.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (12, N'Sam Neill', N'Usa', N'sam-neill.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (13, N'Richard Attenborough', N'Storbritannien', N'richard-attenborough.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (14, N'Laura Dern', N'Usa', N'laura-dern.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (15, N'Jessica Chastain', N'Usa', N'jessica-chastain.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (16, N'Matthew McConaughey', N'Usa', N'matthew-mcconaughey.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (17, N'Jeff Goldblum', N'Usa', N'jeff-goldblum.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (18, N'Sam Worthington', N'Usa', N'sam-worthington.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (19, N'Zoe Saldana', N'Usa', N'zoe-saldana.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (20, N'Michelle Rodriguez', N'Usa', N'michelle-rodriguez.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (21, N'Stephen Lang', N'Usa', N'stephen-lang.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (22, N'Julianne Moore', N'Usa', N'julianne-moore.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (23, N'Pete Postlethwaite', N'Usa', N'pete-postlethwaite.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (24, N'Matt Damon', N'Usa', N'matt-damon.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (25, N'Anne Hathaway', N'Storbritannien', N'anne-hathaway.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (26, N'Michael Caine', N'Usa', N'michael-caine.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (27, N'Mats Bergman', N'Sverige', N'mats-bergman.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (28, N'Jimmy Logan', N'Storbritannien', N'jimmy-logan.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (29, N'Staffan Ling', N'Sverige', N'staffan-ling.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (30, N'Vanessa Lee Chester', N'Usa', N'vanessa-lee-chester.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (31, N'Gösta Ekman', N'Sverige', N'gosta-ekman.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (32, N'Ulf Brunnberg', N'Sverige', N'ulf-brunnberg.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (33, N'Nils Brandt', N'Sverige', N'nils-brandt.webp')
INSERT INTO [dbo].[Actors] ([Id], [Name], [Country], [PictureFilename]) VALUES (34, N'Siw Malmkvist', N'Sverige', N'siw-malmkvist.webp')
SET IDENTITY_INSERT [dbo].[Actors] OFF

SET IDENTITY_INSERT [dbo].[Movies] ON
INSERT INTO [dbo].[Movies] ([Id], [Title], [SubTitle], [Genre], [ReleaseDate], [Rating], [Duration], [Description], [PosterFilename], [PosterFilenameWide], [FromCountry], [Director], [Language], [Budget], [ActorId]) VALUES (1, N'Sällskapsresan', N'eller finns det svenskt kaffe på grisfesten?', N'Komedi', N'1980-08-22 00:00:00', N'8.2', N'1h 47min', N'Sällskapsresan, egentligen Sällskapsresan eller finns det svenskt kaffe på grisfesten, är en svensk komedifilmfrån 1980 i regi av Lasse Åberg. I huvudrollerna ses Lasse Åberg och Jon Skolmen. Det är den första filmen i enserie om och med antihjälten Stig-Helmer Olsson.', N'sallskapsresan.webp', N'sallskapsresan-bred.webp', N'Sverige', N'Lasse Åberg, Peter Hald', N'Svenska', N'3,4 miljoner', NULL)
INSERT INTO [dbo].[Movies] ([Id], [Title], [SubTitle], [Genre], [ReleaseDate], [Rating], [Duration], [Description], [PosterFilename], [PosterFilenameWide], [FromCountry], [Director], [Language], [Budget], [ActorId]) VALUES (2, N'Snowroller', N'Sällskapsresan 2', N'Komedi', N'1985-10-04 00:00:00', N'9.2', N'1h 31min', N'Snowroller - Sällskapsresan 2 är en svenskkomedifilm från 1985 i regi av Lasse Åberg och Peter Hald. I huvudrollerna ses Lasse Åberg och Jon Skolmen. Stig-Helmer Olsson reser tillsammans med Ole Bramserud till schweiziska Alperna för skidsemester, i orten Kirchberg. På resan får de sällskap med ett antal andra personligheter, bland annat familjen Jönsson, den pratglade fabrikören Brännström och dennes tystlåtne kamrer Hedlund från den norrländska landsbygden.;', N'snowroller.webp', N'snowroller-bred.webp', N'Sverige', N'Lasse Åberg, Peter Hald', N'Svenska', N'Ej känt', NULL)
INSERT INTO [dbo].[Movies] ([Id], [Title], [SubTitle], [Genre], [ReleaseDate], [Rating], [Duration], [Description], [PosterFilename], [PosterFilenameWide], [FromCountry], [Director], [Language], [Budget], [ActorId]) VALUES (3, N'Jurassic Park', N'', N'Science fiction, Äventyr', N'1993-05-11 00:00:00', N'9.0', N'2h 2min', N'Jurassic Park är en amerikansk science fiction-äventyrsfilm från 1993, regisserad av Steven Spielberg med manus skrivet av Michael Crichton och David Koepp, baserad på Crichtons roman Urtidsparken från 1990. Filmen, med Sam Neill, Laura Dern, Jeff Goldblum och Richard Attenborough i huvudrollerna, utspelar sig på den fiktiva ön Isla Nublar nära Costa Rica, där den rike affärsmannen John Hammond (Attenborough) och ett team av genetiska forskare har skapat en djurpark med utdöda dinosaurier. När industriellt sabotage leder till en katastrofal avstängning av parkens elektriska stängsel och säkerhetssystem, kämpar en liten grupp besökare för att överleva och fly från den nu farliga ön.', N'jurassic-park.webp', N'jurassic-park-bred.webp', N'Usa', N'Steven Spielberg', N'Engelska', N'163 miljoner dollar', NULL)
INSERT INTO [dbo].[Movies] ([Id], [Title], [SubTitle], [Genre], [ReleaseDate], [Rating], [Duration], [Description], [PosterFilename], [PosterFilenameWide], [FromCountry], [Director], [Language], [Budget], [ActorId]) VALUES (4, N'The lost world', N'Jurassic Park', N'Science fiction, Äventyr', N'1993-05-11 00:00:00', N'9.0', N'2h', N'The Lost World är en amerikansk oscarsnominerad film som hade biopremiär i USA den 23 maj 1997, i regi av Steven Spielberg efter Michael Crichtons roman En försvunnen värld.  Öarna Isla Nublar och Isla Sorna har, efter förödelsen i djurparken "Jurassic Park", lämnats åt sina öden. Parkarbetarna har flytt och den lokala industrin har avvecklats. Det genetikföretag som stod bakom projektet, InGen, står nu inför en finansiell kris. För att undvika konkurs så beslutas det att ön Isla Sorna, där ursprungligen dinosaurierna framavlades, skall exploateras. I stället för att återuppbygga denna enorma attraktion på dessa avlägsna öar företaget har hyrt utanför Costa Ricas kust, så har man tagit det kontroversiella beslutet att flytta djuren till fastlandet i stället, mer bestämt till San Diego, USA. Företagets före detta styrelseordförande John Hammond (Sir Richard Attenborough) har mist kontrollen över företaget och för att värna om de djur han en gång stod som ansvarig för, så skickar han i hemlighet en mindre dokumentärstyrka till ön i ett försök att hinna före InGen:s officiella insatsstyrka. Dessa skall, med kamera och video, dokumentera djuren "i dess naturliga miljö" i ett sista desperat hopp att vända världsopinionen så att isoleringen av De Fem Dödens Öar blir lagskyddade och i fortsättningen förbli obesökta. Men saker går inte som planerat och John Hammonds expedition och InGens expedition blir strandsatta på ön. De båda grupperna måste slå sig samman för att komma hem igen. Men snart inser de att denna förlorade värld hyser hungriga jägare i mörkret och bara de mest utvecklade människorna i gruppen överlever utmaningen ...', N'the-lost-world-jurassic-park.webp', N'the-lost-world-jurassic-park-bred.webp', N'Usa', N'Steven Spielberg', N'Engelska', N'73 miljoner dollar', NULL)
INSERT INTO [dbo].[Movies] ([Id], [Title], [SubTitle], [Genre], [ReleaseDate], [Rating], [Duration], [Description], [PosterFilename], [PosterFilenameWide], [FromCountry], [Director], [Language], [Budget], [ActorId]) VALUES (5, N'Interstellar', N'Jurassic Park', N'Science fiction', N'2014-11-14 00:00:00', N'7.5', N'2h 49min', N'I en nära framtid när människan inte längre kan leva på jordens tillgångar får en grupp forskare och upptäckare det viktigaste uppdraget i mänsklighetens historia: att resa bortom via ett nyupptäckt s.k. maskhål till en annan galax för att ta reda på om mänskligheten genom att flytta dit kan överleva.', N'interstellar.webp', N'interstellar-bred.webp', N'Usa', N'Christopher Nolan', N'Engelska', N'165 miljoner dollar', NULL)
INSERT INTO [dbo].[Movies] ([Id], [Title], [SubTitle], [Genre], [ReleaseDate], [Rating], [Duration], [Description], [PosterFilename], [PosterFilenameWide], [FromCountry], [Director], [Language], [Budget], [ActorId]) VALUES (6, N'Den ofrivillige golfaren', N'Sällskapresan 4', N'Komedi', N'1991-12-20 00:00:00', N'9', N'1h 43min', N'Den ofrivillige golfaren hade biopremiär i Sverige den 25 december 1991. Med Lasse Åberg som regissör och med Lasse Åberg och Jon Skolmen i huvudrollerna är detta den fjärde filmen i serien om Stig-Helmer Olsson.

Det är högkonjunktur i Sverige och Stig-Helmer börjar jobba som gatsopare efter att brödrostfabriken Toastmaster lagts ner av finansbolaget Parvus Finans av rationaliseringsskäl. Under sin första arbetsdag, då han plockar skräp vid en golfbana, får han ett erbjudande; storfinansmannen Bruno Anderhage och galleristen Mette har slagit vad om att vem som helst kan lära sig spela golf lika bra om inte bättre än Bruno på en vecka, och "vem som helst" blir tafatte Stig-Helmer.

Stig-Helmer letar upp morbror Julles gamla golfutrustning från 1920-talet och börjar träna och får hjälp av Ole men det visar sig att det behövs ett smärre mirakel och de beslutar sig för att åka till Skottland och be om hjälp av golflegendaren Roderic McDougall.', N'den-ofrivillige-golfaren.webp', N'den-ofrivillige-golfaren-bred.webp', N'Sverige', N'Lasse Åberg', N'Svenska', N'15-16 miljoner kr', NULL)
INSERT INTO [dbo].[Movies] ([Id], [Title], [SubTitle], [Genre], [ReleaseDate], [Rating], [Duration], [Description], [PosterFilename], [PosterFilenameWide], [FromCountry], [Director], [Language], [Budget], [ActorId]) VALUES (7, N'Varning för Jönsonligan', N'', N'Komedi', N'1981-12-04 00:00:00', N'8,5', N'1h 31min', N'Jönssonligan är en svensk komedifilmserie om en kriminell trio med samma namn.
 Varning för Jönssonligan, hade biopremiär den 4 december 1981 med Charles-Ingvar "Sickan" Jönsson (Gösta Ekman), Ragnar Vanheden (Ulf Brunnberg) och den finlandssvenske Rocky (Nils Brandt) i huvudrollerna.', N'varning-for-jonsonligan.webp', N'varning-for-jonsonligan-bred.webp', N'Sverige', N'Jonas Cornell', N'Svenska', N'4 miljoner kr', NULL)
INSERT INTO [dbo].[Movies] ([Id], [Title], [SubTitle], [Genre], [ReleaseDate], [Rating], [Duration], [Description], [PosterFilename], [PosterFilenameWide], [FromCountry], [Director], [Language], [Budget], [ActorId]) VALUES (8, N'Avatar', N'', N'Science fiction', N'2009-12-18 00:00:00', N'7,5', N'2h 42min', N'Avatar är en amerikansk science fiction-film, regisserad och producerad James Cameron. 
Året är 2154 och människan har tvingats kolonisera och exploatera en ny planet (måne) för att inte gå under på den gamla. Med hjälp av gigantiska ”hell trucks”, förvillande lika de väldiga maskiner som i dagbrott bryter brunkol i Tyskland och de jättelastbilar som fraktar oljesand i Kanada, utvinns grundämnet unobtainium – en nödvändig komponent i jordens energiförsörjning. Ursprungsbefolkningen mördas och skogen skövlas.

Filmens protagonist, Jake Sully (Sam Worthington), är en före detta marinkårssoldat, som skadats i strider på jorden och blivit förlamad från midjan. Han får chansen att delta i Avatar-programmet, genom vilket han ges möjligheten att åter kunna gå normalt.

Genom Avatar-programmet reser Jake till Pandora, en himlakropp täckt av grönskande djungel, fylld av fantastiska livsformer. Pandora är även hem för Na’vi, en humanoid ras som anses primitiv, men som är fysiskt kraftfullare än människor. Na’vi har svans, blå hud, är tre meter långa och lever i harmoni med sin oförstörda värld. Då människorna tränger djupare in i Pandoras skogar på jakt efter värdefulla mineraler, tvingas Na’vi att strida för sin överlevnad.
Jake har ovetandes blivit rekryterad för att fortsätta med övergreppen. Eftersom människor inte kan andas atmosfären på Pandora, har människan genetiskt skapat Avatarerna, vilka är hybrider av människor och Na’vi. Genom sin Avatarkropp återfår Jake full rörelsefrihet och han sänds in i Pandoras djungel som spanare för de soldater som skall komma efter honom. Där upplever han Pandoras skönhet och faror och möter en ung Na’vi-kvinna, Neytiri (Zoe Saldaña) som han förälskar sig i.

På grund av det finner han sig vara fångad mellan det militär-industriella styrkorna från jorden och Na’vi. Han tvingas välja sida i en strid, som kommer att avgöra Pandoras öde.', N'avatar-1.webp', N'avatar-1-bred.webp', N'Usa', N'James Cameron', N'Engelska', N'237 miljoner dollar', NULL)
INSERT INTO [dbo].[Movies] ([Id], [Title], [SubTitle], [Genre], [ReleaseDate], [Rating], [Duration], [Description], [PosterFilename], [PosterFilenameWide], [FromCountry], [Director], [Language], [Budget], [ActorId]) VALUES (9, N'Hälsoresan', N'En smal film av stor vikt', N'Komedi', N'1999-12-25 00:00:00', N'9', N'1h 45min', N'Hälsoresan – En smal film av stor vikt är en svensk komedifilm som hade biopremiär i Sverige den 25 december 1999, i regi av Lasse Åberg med Lasse Åberg och Jon Skolmen i huvudrollerna. Filmen är den femte filmen i serien om Stig Helmer Olsson.', N'halsoresan.webp', N'halsoresan-bred.webp', N'Sverige', N'Lasse Åberg', N'Svenska', N'15-16 miljoner kr', NULL)
SET IDENTITY_INSERT [dbo].[Movies] OFF


SET IDENTITY_INSERT [dbo].[ActorsMovies] ON
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (1, 1, 1)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (2, 1, 2)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (3, 1, 3)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (4, 1, 4)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (5, 1, 5)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (6, 1, 6)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (7, 2, 1)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (8, 2, 2)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (9, 2, 9)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (10, 2, 10)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (11, 2, 11)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (12, 2, 29)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (13, 3, 12)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (14, 3, 13)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (15, 3, 14)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (16, 3, 17)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (17, 4, 13)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (18, 4, 17)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (19, 4, 22)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (20, 4, 23)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (21, 4, 30)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (22, 5, 15)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (23, 5, 16)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (24, 5, 24)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (25, 5, 25)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (26, 5, 26)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (31, 7, 31)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (32, 7, 32)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (33, 7, 33)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (34, 7, 34)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (35, 7, 6)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (36, 8, 18)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (37, 8, 19)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (38, 8, 20)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (39, 8, 21)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (40, 8, 35)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (41, 9, 1)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (42, 9, 2)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (43, 9, 27)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (44, 9, 36)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (45, 6, 1)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (46, 6, 2)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (47, 6, 27)
INSERT INTO [dbo].[ActorsMovies] ([Id], [MoviesId], [ActorsId]) VALUES (48, 6, 28)
SET IDENTITY_INSERT [dbo].[ActorsMovies] OFF

