-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 01, 2025 at 04:18 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `filmdatabase0420`
--

-- --------------------------------------------------------

--
-- Table structure for table `comments`
--

CREATE TABLE `comments` (
  `comment_id` int(11) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `movie_id` int(11) DEFAULT NULL,
  `comment` text NOT NULL,
  `created_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `comments`
--

INSERT INTO `comments` (`comment_id`, `user_id`, `movie_id`, `comment`, `created_at`) VALUES
(1, 1, 1, 'Mind-blowing plot twists!', '2025-03-31 10:00:00'),
(2, 2, 18, 'So funny and heartwarming!', '2025-03-31 10:01:00'),
(3, 3, 10, 'Terrifying classic!', '2025-03-31 10:02:00'),
(4, 4, 6, 'Dinosaurs never get old!', '2025-03-31 10:03:00'),
(5, 5, 7, 'A powerful story.', '2025-03-31 10:04:00'),
(6, 6, 5, 'Amazing action sequences!', '2025-03-31 10:05:00'),
(7, 7, 16, 'Really touching coming-of-age story.', '2025-03-31 10:06:00'),
(8, 8, 58, 'Epic fantasy at its best!', '2025-03-31 10:07:00'),
(9, 9, 29, 'Incredible thriller!', '2025-03-31 10:08:00'),
(10, 10, 4, 'Intense war movie!', '2025-03-31 10:09:00'),
(18, 53, 11, 'gagfaga', '2025-04-22 12:19:15'),
(19, 53, 11, 'faf', '2025-04-22 12:19:54'),
(20, 53, 30, 'fafa', '2025-04-22 12:20:16'),
(21, 53, 11, 'szevasz', '2025-04-22 16:00:14'),
(22, 53, 12, 'vvv', '2025-04-25 15:05:21'),
(26, 55, 1, 'kurvajo film', '2025-04-26 15:10:05'),
(27, 55, 2, 'megolom magam', '2025-04-26 15:17:40'),
(28, 55, 10, 'awdawdawd', '2025-04-26 16:02:31'),
(29, 55, 14, 'goat movie fr', '2025-04-26 17:00:49'),
(30, 55, 7, 'awdawdawdawd', '2025-04-26 19:17:32'),
(31, 55, 3, 'fsefsefsefsef', '2025-04-26 19:17:46'),
(32, 55, 3, 'szep', '2025-04-26 20:59:54'),
(33, 55, 8, 'dawdawdawdawd', '2025-04-26 21:01:36'),
(34, 55, 21, 'awdawdawd', '2025-04-26 21:02:33'),
(35, 55, 21, 'folyik az agyam', '2025-04-26 21:02:39'),
(36, 55, 3, 'zoli', '2025-04-27 10:26:35'),
(37, 55, 8, 'awdawdawdawd', '2025-04-27 11:41:06'),
(38, 55, 3, 'Egyik személyses kedvencem:)', '2025-04-27 18:22:44'),
(39, 55, 40, 'nagy szar', '2025-04-27 18:38:29');

-- --------------------------------------------------------

--
-- Table structure for table `genres`
--

CREATE TABLE `genres` (
  `genre_id` int(11) NOT NULL,
  `genre_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `genres`
--

INSERT INTO `genres` (`genre_id`, `genre_name`) VALUES
(1, 'Action'),
(2, 'Adventure'),
(10, 'Animation'),
(13, 'Biography'),
(3, 'Comedy'),
(11, 'Crime'),
(15, 'Documentary'),
(4, 'Drama'),
(9, 'Fantasy'),
(5, 'Horror'),
(12, 'Mystery'),
(8, 'Romance'),
(6, 'Sci-Fi'),
(7, 'Thriller'),
(14, 'War');

-- --------------------------------------------------------

--
-- Table structure for table `movieactors`
--

CREATE TABLE `movieactors` (
  `movie_id` int(11) NOT NULL,
  `person_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `movieactors`
--

INSERT INTO `movieactors` (`movie_id`, `person_id`) VALUES
(1, 11),
(1, 17),
(1, 27),
(1, 160),
(1, 161),
(2, 17),
(2, 24),
(2, 162),
(2, 163),
(3, 19),
(3, 23),
(3, 27),
(3, 73),
(4, 25),
(4, 27),
(5, 27),
(5, 28),
(6, 41),
(6, 71),
(7, 15),
(7, 41),
(8, 15),
(8, 41),
(9, 15),
(9, 41),
(10, 41),
(10, 71),
(11, 12),
(11, 41),
(12, 12),
(12, 41),
(13, 12),
(13, 41),
(14, 11),
(14, 12),
(15, 12),
(15, 13),
(16, 25),
(16, 26),
(17, 25),
(17, 26),
(18, 13),
(18, 19),
(19, 11),
(19, 23),
(20, 43),
(20, 44),
(21, 29),
(21, 30),
(22, 30),
(22, 43),
(23, 14),
(23, 23),
(24, 14),
(24, 23),
(25, 14),
(25, 23),
(26, 19),
(26, 25),
(27, 19),
(27, 28),
(28, 23),
(28, 52),
(29, 38),
(29, 39),
(30, 38),
(30, 39),
(31, 11),
(31, 12),
(32, 11),
(32, 12),
(33, 11),
(33, 12),
(34, 17),
(34, 24),
(35, 30),
(35, 43),
(36, 38),
(36, 39),
(37, 36),
(37, 38),
(38, 21),
(38, 40),
(39, 42),
(39, 43),
(40, 42),
(40, 43),
(41, 45),
(41, 46),
(42, 49),
(42, 50),
(43, 50),
(43, 51),
(44, 50),
(44, 51),
(45, 54),
(45, 55),
(46, 56),
(46, 57),
(47, 56),
(47, 58),
(48, 60),
(48, 61),
(49, 62),
(49, 63),
(50, 21),
(50, 65),
(51, 21),
(51, 65),
(52, 22),
(52, 70),
(53, 21),
(53, 68),
(54, 80),
(54, 81),
(55, 87),
(55, 88),
(56, 87),
(56, 89),
(57, 87),
(57, 90),
(58, 91),
(58, 92),
(59, 91),
(59, 93),
(60, 91),
(60, 94),
(61, 31),
(61, 32),
(62, 31),
(62, 33),
(63, 31),
(63, 34),
(64, 18),
(64, 19),
(65, 29),
(65, 30),
(66, 30),
(66, 79),
(67, 24),
(67, 27),
(68, 25),
(68, 26),
(69, 26),
(69, 28),
(70, 27),
(70, 28),
(71, 17),
(71, 27);

-- --------------------------------------------------------

--
-- Table structure for table `movies`
--

CREATE TABLE `movies` (
  `movie_id` int(11) NOT NULL,
  `title` varchar(255) NOT NULL,
  `genre_id` int(11) DEFAULT NULL,
  `release_year` int(11) DEFAULT NULL,
  `director_id` int(11) DEFAULT NULL,
  `img_url` varchar(255) NOT NULL,
  `favorite` tinyint(1) NOT NULL DEFAULT 0,
  `duration` int(11) DEFAULT NULL,
  `description` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `movies`
--

INSERT INTO `movies` (`movie_id`, `title`, `genre_id`, `release_year`, `director_id`, `img_url`, `favorite`, `duration`, `description`) VALUES
(1, 'Inception', 1, 2010, 1, 'https://image.tmdb.org/t/p/w1280/9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg', 0, 148, 'An unforgettable journey through dreams and reality.'),
(2, 'The Dark Knight', 1, 2008, 1, 'https://image.tmdb.org/t/p/w1280/qJ2tW6WMUDux911r6m7haRef0WH.jpg', 0, 152, 'A gritty tale of justice, chaos, and inner struggle.'),
(3, 'Interstellar', 6, 2014, 1, 'https://image.tmdb.org/t/p/w1280/gEU2QniE6E77NI6lCU6MxlNBvIx.jpg', 0, 169, 'Exploring space and the limits of human connection.'),
(4, 'Dunkirk', 14, 2017, 1, 'https://image.tmdb.org/t/p/w1280/zx87sFGLXZdrSF4B9WPhwjaKuQP.jpg', 0, 106, 'An intense war survival story filled with courage.'),
(5, 'Tenet', 1, 2020, 1, 'https://image.tmdb.org/t/p/w1280/wRlgk7KLJNUd1Kgct5FimlGgviK.jpg', 0, 150, 'A thrilling take on time and espionage in action.'),
(6, 'Jurassic Park', 2, 1993, 2, 'https://image.tmdb.org/t/p/w1280/fjTU1Bgh3KJu4aatZil3sofR2zC.jpg', 0, 127, 'Science brings dinosaurs back with unexpected results.'),
(7, 'Schindler\'s List', 13, 1993, 2, 'https://image.tmdb.org/t/p/w1280/xx4JCtIkUj31PJbPFRLhuBc1PRl.jpg', 0, 195, 'A powerful depiction of humanity in dark times.'),
(8, 'Saving Private Ryan', 14, 1998, 2, 'https://image.tmdb.org/t/p/w1280/1wY4psJ5NVEhCuOYROwLH2XExM2.jpg', 0, 169, 'One of the most heroic missions in cinematic history.'),
(9, 'E.T. the Extra-Terrestrial', 6, 1982, 2, 'https://image.tmdb.org/t/p/w1280/an0nD6uq6byfxXCfk6lQBzdL2J1.jpg', 0, 115, 'An alien and a boy form an extraordinary friendship.'),
(10, 'Jaws', 5, 1975, 2, 'https://image.tmdb.org/t/p/w1280/lxM6kqilAdpdhqUl2biYp5frUxE.jpg', 0, 124, 'A classic shark attack film that changed cinema forever.'),
(11, 'Pulp Fiction', 11, 1994, 3, 'https://image.tmdb.org/t/p/w1280/d5iIlFn5s0ImszYzBPb8JPIfbXD.jpg', 0, 154, 'A nonlinear narrative filled with iconic performances.'),
(12, 'Kill Bill: Vol. 1', 1, 2003, 3, 'https://image.tmdb.org/t/p/w1280/v7TaX8kXMXs5yFFGR41guUDNcnB.jpg', 0, 111, 'A vengeful bride slashes her way through revenge.'),
(13, 'Inglourious Basterds', 14, 2009, 3, 'https://image.tmdb.org/t/p/w1280/7sfbEnaARXDDhKm0CZ7D7uc2sbo.jpg', 0, 153, 'A band of Jewish soldiers rewriting WWII history.'),
(14, 'Django Unchained', 11, 2012, 3, 'https://image.tmdb.org/t/p/w1280/bV0rWoiRo7pHUTQkh6Oio6irlXO.jpg', 0, 165, 'A bounty hunter unleashes justice in the wild west.'),
(15, 'Once Upon a Time in Hollywood', 11, 2019, 3, 'https://image.tmdb.org/t/p/w1280/8j58iEBw9pOXFD2L0nt0ZXeHviB.jpg', 0, 161, 'Hollywood, nostalgia, and unpredictable storytelling.'),
(16, 'Lady Bird', 4, 2017, 4, 'https://image.tmdb.org/t/p/w1280/gl66K7zRdtNYGrxyS2YDUP5ASZd.jpg', 0, 94, 'A teenager\'s raw journey of rebellion and identity.'),
(17, 'Little Women', 4, 2019, 4, 'https://image.tmdb.org/t/p/w1280/yn5ihODtZ7ofn8pDYfxCmxh8AXI.jpg', 0, 135, 'Sisters growing up in a changing world of tradition.'),
(18, 'Barbie', 3, 2023, 4, 'https://image.tmdb.org/t/p/w1280/iuFNMS8U5cb6xfzi51Dbkovj7vM.jpg', 0, 114, 'A plastic icon takes on the real world in pink style.'),
(19, 'Titanic', 8, 1997, 5, 'https://image.tmdb.org/t/p/w1280/rEPzO9I6LCk6Mxg1X4BsBk6oA3V.jpg', 0, 195, 'A romantic epic aboard a doomed luxury liner.'),
(20, 'Avatar', 6, 2009, 5, 'https://image.tmdb.org/t/p/w1280/kyeqWdyUXW608qlYkRqosgbbJyK.jpg', 0, 162, 'Human and alien worlds collide in stunning visuals.'),
(21, 'Terminator 2: Judgment Day', 1, 1991, 5, 'https://image.tmdb.org/t/p/w1280/5M0j0B18abtBI5gi2RhfjjurTqb.jpg', 0, 137, 'A cyborg returns to protect the future of mankind.'),
(22, 'Aliens', 6, 1986, 5, 'https://image.tmdb.org/t/p/w1280/r1x5JGpyqZU8PYhbs4UcrO1Xb6x.jpg', 0, 137, 'Humans battle terrifying creatures from outer space.'),
(23, 'The Virgin Suicides', 4, 1999, 6, 'https://image.tmdb.org/t/p/w1280/1NCQtXPQnaHRjOZVmktA9BSM35F.jpg', 0, 97, 'Teen girls caught between innocence and obsession.'),
(24, 'Lost in Translation', 4, 2003, 6, 'https://image.tmdb.org/t/p/w1280/wkSzJs7oMf8MIr9CQVICsvRfwA7.jpg', 0, 102, 'Two strangers connect in a foreign land of silence.'),
(25, 'Marie Antoinette', 13, 2006, 6, 'https://image.tmdb.org/t/p/w1280/cybXGmv8Rjd5Os8Xml6YxMBQ0Zt.jpg', 0, 123, 'A queen\'s story wrapped in decadence and loneliness.'),
(26, 'Dune', 6, 2021, 7, 'https://image.tmdb.org/t/p/w1280/d5NXSklXo0qyIYkgV94XAgMIckC.jpg', 0, 155, 'A desert messiah rises in a tale of fate and war.'),
(27, 'Blade Runner 2049', 6, 2017, 7, 'https://image.tmdb.org/t/p/w1280/gajva2L0rPYkEWjzgFlBXCAVBE5.jpg', 0, 164, 'A detective in a neon future seeks hidden truths.'),
(28, 'Arrival', 6, 2016, 7, 'https://image.tmdb.org/t/p/w1280/x2FJsf1ElAgr63Y3PNPtJrcmpoe.jpg', 0, 116, 'Language becomes a weapon of peace or war.'),
(29, 'Parasite', 7, 2019, 8, 'https://image.tmdb.org/t/p/w1280/7IiTTgloJzvGI1TAYymCfbfl3vT.jpg', 0, 132, 'A family and a secret in a social satire thriller.'),
(30, 'Memories of Murder', 11, 2003, 8, 'https://image.tmdb.org/t/p/w1280/jcgUjx1QcupGzjntTVlnQ15lHqy.jpg', 0, 132, 'Detectives pursue a killer in small-town Korea.'),
(31, 'The Wolf of Wall Street', 11, 2013, 9, 'https://image.tmdb.org/t/p/w1280/kW9LmvYHAaS9iA0tHmZVq8hQYoq.jpg', 0, 180, 'A wild ride through wealth and excess.'),
(32, 'Goodfellas', 11, 1990, 9, 'https://image.tmdb.org/t/p/w1280/aKuFiU82s5ISJpGZp7YkIr3kCUd.jpg', 0, 146, 'A gripping tale of loyalty and betrayal.'),
(33, 'The Departed', 11, 2006, 9, 'https://image.tmdb.org/t/p/w1280/aqfeqtqnuCk2zCG7QxVkvm7kf1O.jpg', 0, 151, 'A tense undercover clash of cops and criminals.'),
(34, 'Gladiator', 1, 2000, 10, 'https://image.tmdb.org/t/p/w1280/ty8TGRuvJLPUmAR1H1nRIsgwvim.jpg', 0, 155, 'A warrior fights for vengeance and honor.'),
(35, 'Alien', 5, 1979, 10, 'https://image.tmdb.org/t/p/w1280/vfrQk5IPloGg1v9Rzbh2Eg3VGyM.jpg', 0, 117, 'A chilling encounter with terror in deep space.'),
(36, 'Black Panther', 1, 2018, 134, 'https://image.tmdb.org/t/p/w1280/uxzzxijgPIY7slzFvMotPv8wjKA.jpg', 0, 134, 'A vibrant celebration of culture and heroism.'),
(37, 'Get Out', 5, 2017, 128, 'https://image.tmdb.org/t/p/w1280/tFXcEccSQMf3lfhfXKSU9iRBpa3.jpg', 0, 104, 'A chilling exploration of race and hidden horrors.'),
(38, 'Captain Marvel', 1, 2019, 119, 'https://image.tmdb.org/t/p/w1280/AtsgWhDnHTq68L0lLsUrCnM7TjG.jpg', 0, 123, 'A cosmic journey of power and self-discovery.'),
(39, 'Guardians of the Galaxy', 1, 2014, 121, 'https://image.tmdb.org/t/p/w1280/jPrJPZKJVhvyJ4DmUTrDgmFN0yG.jpg', 0, 121, 'A ragtag crew adventures across the stars.'),
(40, 'Guardians of the Galaxy Vol. 2', 1, 2017, 121, 'https://image.tmdb.org/t/p/w1280/y4MBh0EjBlMuOzv9axM4qJlmhzz.jpg', 0, 136, 'A cosmic family faces chaos and connection.'),
(41, 'Fast & Furious 7', 1, 2015, 103, 'https://image.tmdb.org/t/p/w1280/wurKlC3VKUgcfsn0K51MJYEleS2.jpg', 0, 137, 'A high-octane chase fueled by family and fury.'),
(42, 'Wonder Woman', 1, 2017, 104, 'https://image.tmdb.org/t/p/w1280/v4ncgZjG2Zu8ZW5al1vIZTsSjqX.jpg', 0, 141, 'A warrior goddess battles for justice.'),
(43, 'Man of Steel', 1, 2013, 105, 'https://image.tmdb.org/t/p/w1280/dksTL9NXc3GqPBRHYHcy1aIwjS.jpg', 0, 143, 'A superhero struggles to save a doomed world.'),
(44, 'Batman v Superman: Dawn of Justice', 1, 2016, 105, 'https://image.tmdb.org/t/p/w1280/5UsK3grJvtQrtzEgqNlDljJW96w.jpg', 0, 151, 'Heroes collide in a battle for truth.'),
(45, 'The Amazing Spider-Man', 1, 2012, 122, 'https://image.tmdb.org/t/p/w1280/3zbb5XD0hEA8IpAcUmv1J9Rej3B.jpg', 0, 136, 'A young hero swings into action and identity.'),
(46, 'Spider-Man: Homecoming', 1, 2017, 106, 'https://image.tmdb.org/t/p/w1280/c24sv2weTHPsmDa7jEMN0m2P3RT.jpg', 0, 133, 'A teen hero balances school and saving the world.'),
(47, 'Spider-Man: No Way Home', 1, 2021, 106, 'https://image.tmdb.org/t/p/w1280/fVzXp3NwovUlLe7fvoRynCmBPNc.jpg\n', 0, 148, 'A multiverse adventure of heroes and heartbreak.'),
(48, 'Doctor Strange', 1, 2016, 123, 'https://image.tmdb.org/t/p/w1280/hEyHVgBE28Jl13h2Qpbr3LH77sP.jpg', 0, 115, 'A mystic journey through time and reality.'),
(49, 'Ant-Man', 1, 2015, 124, 'https://image.tmdb.org/t/p/w1280/rQRnQfUl3kfp78nCWq8Ks04vnq1.jpg', 0, 117, 'A small hero takes on big heists and heart.'),
(50, 'Avengers: Infinity War', 1, 2018, 107, 'https://image.tmdb.org/t/p/w1280/1uoU4qPZEcOus0rQf8Tz4JCn3cX.jpg', 0, 149, 'Heroes unite against an unstoppable cosmic threat.'),
(51, 'Avengers: Endgame', 1, 2019, 107, 'https://image.tmdb.org/t/p/w1280/ge8hewUzdoAnxRWNzHQ8naQiBcG.jpg', 0, 181, 'A final stand to save the universe.'),
(52, 'Thor: Ragnarok', 1, 2017, 72, 'https://image.tmdb.org/t/p/w1280/rzRwTcFvttcN1ZpX2xv4j3tSdJu.jpg', 0, 130, 'A thunderous clash of gods and rebellion.'),
(53, 'Iron Man', 1, 2008, 69, 'https://image.tmdb.org/t/p/w1280/78lPtwv72eTNqFW9COBYI0dWDJa.jpg', 0, 126, 'A genius builds a hero from iron and ambition.'),
(54, 'Captain America: The First Avenger', 1, 2011, 126, 'https://image.tmdb.org/t/p/w1280/vSNxAJTlD0r02V9sPYpOjqDZXUK.jpg', 0, 124, 'A soldier\'s origin in a war for freedom.'),
(55, 'The Hobbit: An Unexpected Journey', 2, 2012, 153, 'https://image.tmdb.org/t/p/w1280/yHA9Fc37VmpUA5UncTxxo3rTGVA.jpg', 0, 169, 'A humble hobbit begins an epic quest.'),
(56, 'The Hobbit: The Desolation of Smaug', 2, 2013, 153, 'https://image.tmdb.org/t/p/w1280/xQYiXsheRCDBA39DOrmaw1aSpbk.jpg', 0, 161, 'A dragon\'s wrath threatens a daring adventure.'),
(57, 'The Hobbit: The Battle of the Five Armies', 2, 2014, 153, 'https://m.media-amazon.com/images/I/91C7PlfKJGL._AC_SY679_.jpg', 0, 144, 'A climactic battle for Middle-earth\'s fate.'),
(58, 'The Lord of the Rings: The Fellowship of the Ring', 9, 2001, 153, 'https://image.tmdb.org/t/p/w1280/6oom5QYQ2yQTMJIbnvbkL0tec.jpg', 0, 178, 'A fellowship unites to destroy a dark power.'),
(59, 'The Lord of the Rings: The Two Towers', 9, 2002, 153, 'https://image.tmdb.org/t/p/w1280/5VTN0pR8gcqV3EPUHHfMGnJYN9L.jpg', 0, 179, 'A war escalates as heroes face impossible odds.'),
(60, 'The Lord of the Rings: The Return of the King', 9, 2003, 153, 'https://image.tmdb.org/t/p/w1280/rCzpDGLbOoPwLjy3OAm5NUPOTrC.jpg', 0, 201, 'A king rises to end an age of darkness.'),
(61, 'Star Wars: The Force Awakens', 6, 2015, 154, 'https://image.tmdb.org/t/p/w1280/wqnLdwVXoBjKibFRR5U3y0aDUhs.jpg', 0, 138, 'A new hope sparks in a galaxy far away.'),
(62, 'Star Wars: The Last Jedi', 6, 2017, 132, 'https://image.tmdb.org/t/p/w1280/ySaaKHOLAQU5HoZqWmzDIj1VvZ1.jpg', 0, 152, 'A jedi faces tests of loyalty and legacy.'),
(63, 'Star Wars: The Rise of Skywalker', 6, 2019, 154, 'https://image.tmdb.org/t/p/w1280/db32LaOibwEliAmSL2jjDF6oDdj.jpg', 0, 142, 'A saga ends with battles and redemption.'),
(64, 'La La Land', 8, 2016, 130, 'https://image.tmdb.org/t/p/w1280/uDO8zWDhfWwoFdKS4fzkUJt0Rf0.jpg', 0, 128, 'A dazzling dream of love and ambition.'),
(65, 'The Matrix', 6, 1999, 155, 'https://image.tmdb.org/t/p/w1280/dXNAPwY7VrqMAo51EKhhCJfaGb5.jpg', 0, 136, 'A hacker uncovers a reality beyond the code.'),
(66, 'Mad Max: Fury Road', 1, 2015, 127, 'https://image.tmdb.org/t/p/w1280/hA2ple9q4qnwxp3hKVNhroipsir.jpg', 0, 120, 'A relentless chase in a post-apocalyptic wasteland.'),
(67, 'Joker', 7, 2019, 133, 'https://image.tmdb.org/t/p/w1280/udDclJoHjfjb8Ekgsd4FDteOkCU.jpg', 0, 122, 'A broken mind descends into chaos and tragedy.'),
(69, 'Midsommar', 5, 2019, 129, 'https://image.tmdb.org/t/p/w1280/7LEI8ulZzO5gy9Ww2NVCrKmHeDZ.jpg', 0, 148, 'A bright festival hides a dark twisted ritual.'),
(70, 'Knives Out', 12, 2019, 132, 'https://image.tmdb.org/t/p/w1280/pThyQovXQrw2m0s9x82twj48Jq4.jpg', 0, 130, 'A sharp whodunit with secrets and surprises.'),
(71, 'Oppenheimer', 13, 2023, 1, 'https://image.tmdb.org/t/p/w1280/ljsZTbVsrQSqZgWeep2B1QiDKuh.jpg', 0, 180, 'A scientist shapes a world at war.'),
(72, 'The Grand Budapest Hotel', 3, 2014, 102, 'https://image.tmdb.org/t/p/w1280/eWdyYQreja6JGCzqHWXpWHDrrPo.jpg', 0, 99, 'A quirky tale of loyalty in a colorful world.'),
(73, 'The Nice Guys', 3, 2016, 103, 'https://image.tmdb.org/t/p/w1280/clq4So9spa9cXk3MZy2iMdqkxP2.jpg', 0, 116, 'A mismatched duo unravels a gritty mystery.'),
(74, 'Superbad', 3, 2007, 104, 'https://image.tmdb.org/t/p/w1280/ek8e8txUyUwd2BNqj6lFEerJfbq.jpg', 0, 113, 'A raunchy night of friendship and chaos.'),
(75, 'The Hangover', 3, 2009, 105, 'https://image.tmdb.org/t/p/w1280/A0uS9rHR56FeBtpjVki16M5xxSW.jpg', 0, 100, 'A wild night spirals into hilarious mayhem.'),
(76, 'Deadpool', 3, 2016, 106, 'https://image.tmdb.org/t/p/w1280/3E53WEZJqP6aM84D8CckXx4pIHw.jpg', 0, 108, 'A foul-mouthed hero fights with heart and humor.'),
(85, 'Inception', 6, 2010, 123, 'https://image.tmdb.org/t/p/w1280/9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg', 0, 148, 'A mind-bending heist through layers of dreams.'),
(86, 'Se7en', 6, 1995, 124, 'https://image.tmdb.org/t/p/w1280/6yoghtyTpznpBik8EngEmJskVUO.jpg', 0, 127, 'A dark hunt for a killer in a world of sin.'),
(87, 'Shutter Island', 6, 2010, 9, 'https://image.tmdb.org/t/p/w1280/kve20tXwUZpu4GUX8l6X7Z4jmL6.jpg', 0, 138, 'A mind unravels on a haunting island mystery.'),
(88, 'Prisoners', 6, 2013, 7, 'https://image.tmdb.org/t/p/w1280/bEReHHjxYtR3n84QkXGFtS1cNqM.jpg', 0, 153, 'A desperate search for a child in a web of lies.'),
(89, 'Mission: Impossible', 6, 1996, 127, 'https://image.tmdb.org/t/p/w1280/s2bT29y0ngXxxu2IA8AOzzXTRhd.jpg', 0, 110, 'A spy takes on an impossible global mission.'),
(90, 'Skyfall', 6, 2012, 128, 'https://image.tmdb.org/t/p/w1280/izrHg2UzxG3YXtbOS0P6sW80YPS.jpg', 0, 143, 'A suave agent battles foes and inner demons.'),
(91, 'Psycho', 6, 1960, 129, 'https://image.tmdb.org/t/p/w1280/81d8oyEFgj7FlxJqSDXWr8JH8kV.jpg', 0, 109, 'A motel hides secrets of madness and murder.'),
(92, 'Oldboy', 6, 2003, 130, 'https://image.tmdb.org/t/p/w1280/pWDtjs568ZfOTMbURB0FgpjJC4E.jpg', 0, 120, 'A man seeks vengeance with shocking revelations.'),
(93, 'Hereditary', 5, 2018, 139, 'https://image.tmdb.org/t/p/w1280/4GFPuL14eXi66V96xBWY73Y9PfR.jpg', 0, 127, 'A family unravels under a sinister curse.'),
(94, 'The Conjuring', 5, 2013, 103, 'https://image.tmdb.org/t/p/w1280/wVYREutTvI2tmxr6ujrHT704wGF.jpg', 0, 112, 'A haunted house traps a family in terror.'),
(95, 'A Quiet Place', 5, 2018, 118, 'https://image.tmdb.org/t/p/w1280/nAU74GmpUk7t5iklEp3bufwDq4n.jpg', 0, 90, 'Silence becomes survival in a world of monsters.'),
(96, 'It Follows', 5, 2014, 119, 'https://image.tmdb.org/t/p/w1280/hll4O5vSAfnZDb6JbnP06GPtz7b.jpg', 0, 100, 'A curse stalks its prey with relentless dread.'),
(97, 'It', 5, 2017, 120, 'https://image.tmdb.org/t/p/w1280/9E2y5Q7WlCVNEhP5GiVTjhEhx1o.jpg', 0, 135, 'A clown brings terror to a town of brave kids.'),
(98, 'Doctor Sleep', 5, 2019, 121, 'https://image.tmdb.org/t/p/w1280/p69QzIBbN06aTYqRRiCOY1emNBh.jpg', 0, 152, 'A psychic battles demons from a dark past.'),
(99, 'The Witch', 5, 2015, 122, 'https://image.tmdb.org/t/p/w1280/zap5hpFCWSvdWSuPGAQyjUv2wAC.jpg', 0, 92, 'A puritan family faces evil in the wilderness.'),
(108, 'Before Sunrise', 8, 1995, 131, 'https://image.tmdb.org/t/p/w1280/kf1Jb1c2JAOqjuzA3H4oDM263uB.jpg', 0, 101, 'A fleeting night of love and endless talks.'),
(109, 'Little Women', 8, 2019, 4, 'https://image.tmdb.org/t/p/w1280/yn5ihODtZ7ofn8pDYfxCmxh8AXI.jpg', 0, 135, 'Sisters weave love and dreams in a classic tale.'),
(110, 'Call Me by Your Name', 8, 2017, 132, 'https://image.tmdb.org/t/p/w1280/gXiE0WveDnT0n5J4sW9TMxXF4oT.jpg', 0, 132, 'A summer romance blooms with timeless passion.'),
(111, 'Romeo + Juliet', 8, 1996, 133, 'https://image.tmdb.org/t/p/w1280/vaBQKLbSWkXGTOlsz9ARdJP4lvg.jpg', 0, 120, 'A modern tragedy of star-crossed lovers.'),
(112, 'Titanic', 8, 1997, 5, 'https://image.tmdb.org/t/p/w1280/9xjZS2rlVxm8SFx8kPC3aIGCOYQ.jpg', 0, 195, 'A doomed love story aboard a sinking ship.'),
(113, '(500) Days of Summer', 8, 2009, 134, 'https://image.tmdb.org/t/p/w1280/qXAuQ9hF30sQRsXf40OfRVl0MJZ.jpg', 0, 95, 'A quirky romance of love and messy truths.'),
(114, 'Pride & Prejudice', 8, 2005, 137, 'https://image.tmdb.org/t/p/w1280/sGjIvtVvTlWnia2zfJfHz81pZ9Q.jpg', 0, 129, 'A spirited clash of love and pride.'),
(115, 'The Notebook', 8, 2004, 138, 'https://image.tmdb.org/t/p/w1280/rNzQyW4f8B8cQeg7Dgj3n6eT5k9.jpg', 0, 123, 'A timeless love defies fate and memory.'),
(116, 'Se7en', 7, 1995, 139, 'https://image.tmdb.org/t/p/w1280/69Sns8WoET6CfaYlIkHbla4l7nC.jpg', 0, 127, 'A grim chase for a killer in the shadows.'),
(117, 'Prisoners', 7, 2013, 7, 'https://image.tmdb.org/t/p/w1280/uhviyknTT5cEQXbn6vWIqfM4vGm.jpg', 0, 153, 'A father searches for truth in a dark mystery.'),
(118, 'Zodiac', 7, 2007, 139, 'https://image.tmdb.org/t/p/w1280/6YmeO4pB7XTh8P8F960O1uA14JO.jpg', 0, 157, 'A journalist hunts a killer through decades.'),
(119, 'Oldboy', 7, 2003, 142, 'https://image.tmdb.org/t/p/w1280/pWDtjs568ZfOTMbURQBYuT4Qxka.jpg', 0, 120, 'A captive seeks revenge with shocking twists.'),
(120, 'The Girl with the Dragon Tattoo', 7, 2011, 139, 'https://image.tmdb.org/t/p/w1280/vbLedKc1BUF4FOH1GyHW62FulCc.jpg', 0, 158, 'A hacker uncovers secrets in a twisted mystery.'),
(121, 'Shutter Island', 7, 2010, 9, 'https://image.tmdb.org/t/p/w1280/kve20tXwUZpu4GUX8l6X7Z4jmL6.jpg', 0, 138, 'A man grapples with reality on a cursed island.'),
(122, 'Split', 7, 2016, 144, 'https://image.tmdb.org/t/p/w1280/lli31lYTFpvxVBeFHWoe5PMfW5s.jpg', 0, 117, 'A fractured mind hides a terrifying secret.'),
(123, 'Egy Film', 2, 2025, NULL, 'https.:/kep.jpg', 0, NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `persons`
--

CREATE TABLE `persons` (
  `person_id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `role` varchar(20) NOT NULL,
  `birth_date` date DEFAULT NULL,
  `birth_place` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `persons`
--

INSERT INTO `persons` (`person_id`, `name`, `role`, `birth_date`, `birth_place`) VALUES
(1, 'Christopher Nolan', 'Director', '1970-07-30', 'London, UK'),
(2, 'Steven Spielberg', 'Director', '1946-12-18', 'Cincinnati, OH, USA'),
(3, 'Quentin Tarantino', 'Director', '1963-03-27', 'Knoxville, TN, USA'),
(4, 'Greta Gerwig', 'Director', '1983-08-04', 'Sacramento, CA, USA'),
(5, 'James Cameron', 'Director', '1954-08-16', 'Kapuskasing, Canada'),
(6, 'Sofia Coppola', 'Director', '1971-05-14', 'New York, NY, USA'),
(7, 'Denis Villeneuve', 'Director', '1967-10-03', 'Quebec, Canada'),
(8, 'Bong Joon-ho', 'Director', '1969-09-14', 'Daegu, South Korea'),
(9, 'Martin Scorsese', 'Director', '1942-11-17', 'Queens, NY, USA'),
(10, 'Ridley Scott', 'Director', '1937-11-30', 'South Shields, UK'),
(11, 'Leonardo DiCaprio', 'Actor', '1974-11-11', 'Los Angeles, CA, USA'),
(12, 'Brad Pitt', 'Actor', '1963-12-18', 'Shawnee, OK, USA'),
(13, 'Margot Robbie', 'Actor', '1990-07-02', 'Dalby, Australia'),
(14, 'Scarlett Johansson', 'Actor', '1984-11-22', 'New York, NY, USA'),
(15, 'Tom Hanks', 'Actor', '1956-07-09', 'Concord, CA, USA'),
(16, 'Natalie Portman', 'Actor', '1981-06-09', 'Jerusalem, Israel'),
(17, 'Christian Bale', 'Actor', '1974-01-30', 'Haverfordwest, UK'),
(18, 'Emma Stone', 'Actor', '1988-11-06', 'Scottsdale, AZ, USA'),
(19, 'Ryan Gosling', 'Actor', '1980-11-12', 'London, Canada'),
(20, 'Zendaya', 'Actor', '1996-09-01', 'Oakland, CA, USA'),
(21, 'Robert Downey Jr.', 'Actor', '1965-04-04', 'New York, NY, USA'),
(22, 'Chris Hemsworth', 'Actor', '1983-08-11', 'Melbourne, Australia'),
(23, 'Anne Hathaway', 'Actor', '1982-11-12', 'Brooklyn, NY, USA'),
(24, 'Joaquin Phoenix', 'Actor', '1974-10-28', 'San Juan, Puerto Rico'),
(25, 'Timothée Chalamet', 'Actor', '1995-12-27', 'New York, NY, USA'),
(26, 'Florence Pugh', 'Actor', '1996-01-03', 'Oxford, UK'),
(27, 'Cillian Murphy', 'Actor', '1976-05-25', 'Cork, Ireland'),
(28, 'Ana de Armas', 'Actor', '1988-04-30', 'Havana, Cuba'),
(29, 'Keanu Reeves', 'Actor', '1964-09-02', 'Beirut, Lebanon'),
(30, 'Charlize Theron', 'Actor', '1975-08-07', 'Benoni, South Africa'),
(31, 'John Boyega', 'Actor', '1992-03-17', 'London, UK'),
(32, 'Daisy Ridley', 'Actor', '1992-04-10', 'London, UK'),
(33, 'Adam Driver', 'Actor', '1983-11-19', 'San Diego, CA, USA'),
(34, 'Oscar Isaac', 'Actor', '1979-03-09', 'Guatemala City, Guatemala'),
(35, 'Lupita Nyong\'o', 'Actor', '1983-03-01', 'Mexico City, Mexico'),
(36, 'Daniel Kaluuya', 'Actor', '1989-02-24', 'London, UK'),
(37, 'Letitia Wright', 'Actor', '1993-10-31', 'Georgetown, Guyana'),
(38, 'Chadwick Boseman', 'Actor', '1976-11-29', 'Anderson, SC, USA'),
(39, 'Michael B. Jordan', 'Actor', '1987-02-09', 'Santa Ana, CA, USA'),
(40, 'Brie Larson', 'Actor', '1989-10-01', 'Sacramento, CA, USA'),
(41, 'Samuel L. Jackson', 'Actor', '1948-12-21', 'Washington, D.C., USA'),
(42, 'Chris Pratt', 'Actor', '1979-06-21', 'Virginia, MN, USA'),
(43, 'Zoe Saldaña', 'Actor', '1978-06-19', 'Passaic, NJ, USA'),
(44, 'Dave Bautista', 'Actor', '1969-01-18', 'Washington, D.C., USA'),
(45, 'Vin Diesel', 'Actor', '1967-07-18', 'Alameda County, CA, USA'),
(46, 'Michelle Rodriguez', 'Actor', '1978-07-12', 'San Antonio, TX, USA'),
(47, 'Jason Statham', 'Actor', '1967-07-26', 'Shirebrook, UK'),
(48, 'Dwayne Johnson', 'Actor', '1972-05-02', 'Hayward, CA, USA'),
(49, 'Gal Gadot', 'Actor', '1985-04-30', 'Petah Tikva, Israel'),
(50, 'Henry Cavill', 'Actor', '1983-05-05', 'Saint Helier, Jersey'),
(51, 'Ben Affleck', 'Actor', '1972-08-15', 'Berkeley, CA, USA'),
(52, 'Amy Adams', 'Actor', '1974-08-20', 'Vicenza, Italy'),
(53, 'Jesse Eisenberg', 'Actor', '1983-10-05', 'Queens, NY, USA'),
(54, 'Andrew Garfield', 'Actor', '1983-08-20', 'Los Angeles, CA, USA'),
(55, 'Tobey Maguire', 'Actor', '1975-06-27', 'Santa Monica, CA, USA'),
(56, 'Tom Holland', 'Actor', '1996-06-01', 'London, UK'),
(57, 'Kirsten Dunst', 'Actor', '1982-04-30', 'Point Pleasant, NJ, USA'),
(58, 'Willem Dafoe', 'Actor', '1955-07-22', 'Appleton, WI, USA'),
(59, 'Alfred Molina', 'Actor', '1953-05-24', 'London, UK'),
(60, 'Benedict Cumberbatch', 'Actor', '1976-07-19', 'London, UK'),
(61, 'Elizabeth Olsen', 'Actor', '1989-02-16', 'Sherman Oaks, CA, USA'),
(62, 'Paul Rudd', 'Actor', '1969-04-06', 'Passaic, NJ, USA'),
(63, 'Evangeline Lilly', 'Actor', '1979-08-03', 'Fort Saskatchewan, Canada'),
(64, 'Michael Keaton', 'Actor', '1951-09-05', 'Coraopolis, PA, USA'),
(65, 'Mark Ruffalo', 'Actor', '1967-11-22', 'Kenosha, WI, USA'),
(66, 'Don Cheadle', 'Actor', '1964-11-29', 'Kansas City, MO, USA'),
(67, 'Jeremy Renner', 'Actor', '1971-01-07', 'Modesto, CA, USA'),
(68, 'Gwyneth Paltrow', 'Actor', '1972-09-27', 'Los Angeles, CA, USA'),
(69, 'Jon Favreau', 'Director', '1966-10-19', 'Queens, NY, USA'),
(70, 'Tessa Thompson', 'Actor', '1983-10-03', 'Los Angeles, CA, USA'),
(71, 'Jeff Goldblum', 'Actor', '1952-10-22', 'West Homestead, PA, USA'),
(72, 'Taika Waititi', 'Director', '1975-08-16', 'Wellington, New Zealand'),
(73, 'Rachel McAdams', 'Actor', '1978-11-17', 'London, Canada'),
(74, 'Mads Mikkelsen', 'Actor', '1965-11-22', 'Copenhagen, Denmark'),
(75, 'Tilda Swinton', 'Actor', '1960-11-05', 'London, UK'),
(76, 'Idris Elba', 'Actor', '1972-09-06', 'London, UK'),
(77, 'Tom Hiddleston', 'Actor', '1981-02-09', 'London, UK'),
(78, 'Anthony Hopkins', 'Actor', '1937-12-31', 'Port Talbot, UK'),
(79, 'Cate Blanchett', 'Actor', '1969-05-14', 'Melbourne, Australia'),
(80, 'Chris Evans', 'Actor', '1981-06-13', 'Boston, MA, USA'),
(81, 'Sebastian Stan', 'Actor', '1982-08-13', 'Constanța, Romania'),
(82, 'Hayley Atwell', 'Actor', '1982-04-05', 'London, UK'),
(83, 'Hugo Weaving', 'Actor', '1960-04-04', 'Ibadan, Nigeria'),
(84, 'Tommy Lee Jones', 'Actor', '1946-09-15', 'San Saba, TX, USA'),
(85, 'Stanley Tucci', 'Actor', '1960-11-11', 'Peekskill, NY, USA'),
(86, 'Dominic Cooper', 'Actor', '1978-06-02', 'London, UK'),
(87, 'Richard Armitage', 'Actor', '1971-08-22', 'Leicester, UK'),
(88, 'Andy Serkis', 'Actor', '1964-04-20', 'Ruislip, UK'),
(89, 'Martin Freeman', 'Actor', '1971-09-08', 'Aldershot, UK'),
(90, 'Ian McKellen', 'Actor', '1939-05-25', 'Burnley, UK'),
(91, 'Elijah Wood', 'Actor', '1981-01-28', 'Cedar Rapids, IA, USA'),
(92, 'Sean Astin', 'Actor', '1971-02-25', 'Santa Monica, CA, USA'),
(93, 'Viggo Mortensen', 'Actor', '1958-10-20', 'New York, NY, USA'),
(94, 'Orlando Bloom', 'Actor', '1977-01-13', 'Canterbury, UK'),
(95, 'Liv Tyler', 'Actor', '1977-07-01', 'New York, NY, USA'),
(96, 'John Rhys-Davies', 'Actor', '1944-05-05', 'Salisbury, UK'),
(97, 'Billy Boyd', 'Actor', '1968-08-28', 'Glasgow, UK'),
(98, 'Dominic Monaghan', 'Actor', '1976-12-08', 'Berlin, Germany'),
(99, 'Aidan Turner', 'Actor', '1983-06-19', 'Dublin, Ireland'),
(100, 'Dean OGorman', 'Actor', '1976-12-01', 'Auckland, New Zealand'),
(101, 'Luke Evans', 'Actor', '1979-04-15', 'Pontypool, UK'),
(102, 'Wes Anderson', 'Director', '1978-04-02', 'Szombathely, HU'),
(103, 'James Wan', 'Director', '1977-02-26', 'Kuching, Malaysia'),
(104, 'Patty Jenkins', 'Director', '1971-07-24', 'Victorville, California, USA'),
(105, 'Zack Snyder', 'Director', '1966-03-01', 'Green Bay, Wisconsin, USA'),
(106, 'Jon Watts', 'Director', '1981-06-28', 'Fountain, Colorado, USA'),
(107, 'Anthony Russo', 'Director', '1970-02-03', 'Cleveland, Ohio, USA'),
(108, 'Joe Russo', 'Director', '1971-07-18', 'Cleveland, Ohio, USA'),
(118, 'John Krasinski', 'Director', '1978-07-18', 'Szombathely, HUú'),
(119, 'Anna Boden', 'Director', '1976-09-20', 'USA'),
(120, 'Ryan Fleck', 'Director', '1976-09-20', 'USA'),
(121, 'James Gunn', 'Director', '1966-08-05', 'St. Louis, Missouri, USA'),
(122, 'Marc Webb', 'Director', '1974-08-31', 'Bloomington, Indiana, USA'),
(123, 'Scott Derrickson', 'Director', '1966-07-16', 'Denver, Colorado, USA'),
(124, 'Peyton Reed', 'Director', '1964-07-03', 'Raleigh, North Carolina, USA'),
(126, 'Joe Johnston', 'Director', '1950-05-13', 'Austin, Texas, USA'),
(127, 'George Miller', 'Director', '1945-03-03', 'Brisbane, Australia'),
(128, 'Jordan Peele', 'Director', '1979-02-21', 'New York City, USA'),
(129, 'Ari Aster', 'Director', '1986-07-15', 'New York City, USA'),
(130, 'Damien Chazelle', 'Director', '1985-01-19', 'Providence, Rhode Island, USA'),
(131, 'Luca Guadagnino', 'Director', '1971-08-10', 'Palermo, Sicily, Italy'),
(132, 'Rian Johnson', 'Director', '1973-12-17', 'Silver Spring, Maryland, USA'),
(133, 'Todd Phillips', 'Director', '1970-12-20', 'Brooklyn, New York City, USA'),
(134, 'Ryan Coogler', 'Director', '1970-12-20', 'Szombathely, HUN'),
(137, 'Joe Wright', 'Director', '1972-08-25', 'London, UK'),
(138, 'Nick Cassavetes', 'Director', '1959-05-21', 'New York City, NY, USA'),
(139, 'David Fincher', 'Director', '1962-08-28', 'Denver, CO, USA'),
(142, 'Chan-wook Park', 'Director', '1963-08-23', 'Seoul, South Korea'),
(144, 'M. Night Shyamalan', 'Director', '1970-08-06', 'Puducherry, India'),
(153, 'Peter Jackson', 'Director', '1961-10-31', 'Pukerua Bay, New Zealand'),
(154, 'J.J. Abrams', 'Director', '1966-06-27', 'New York City, NY, USA'),
(155, 'Lana Wachowski', 'Director', '1965-06-21', 'Chicago, IL, USA'),
(156, 'Lilly Wachowski', 'Director', '1967-12-29', 'Chicago, IL, USA'),
(160, 'Vera Farmiga', 'Actor', '1973-08-06', 'Clifton, NJ, USA'),
(161, 'Patrick Wilson', 'Actor', '1973-07-03', 'Norfolk, VA, USA'),
(162, 'Keira Knightley', 'Actor', '1985-03-26', 'Teddington, UK'),
(163, 'Matthew Macfadyen', 'Actor', '1974-10-17', 'Great Yarmouth, UK'),
(174, 'Kis Pista', 'Actor', '1975-07-26', 'Szombathely');

-- --------------------------------------------------------

--
-- Table structure for table `ratings`
--

CREATE TABLE `ratings` (
  `rating_id` int(11) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `movie_id` int(11) DEFAULT NULL,
  `rating` tinyint(4) DEFAULT NULL CHECK (`rating` between 1 and 5)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `ratings`
--

INSERT INTO `ratings` (`rating_id`, `user_id`, `movie_id`, `rating`) VALUES
(1, 1, 1, 5),
(2, 1, 3, 4),
(3, 1, 5, 3),
(4, 2, 18, 5),
(5, 2, 19, 4),
(6, 2, 64, 3),
(7, 3, 10, 4),
(8, 3, 35, 5),
(9, 3, 37, 3),
(10, 4, 6, 5),
(11, 4, 55, 4),
(12, 4, 58, 3),
(13, 5, 7, 5),
(14, 5, 25, 4),
(15, 5, 31, 3),
(16, 6, 1, 4),
(17, 6, 5, 5),
(18, 6, 21, 3),
(19, 7, 16, 5),
(20, 7, 17, 4),
(21, 7, 23, 3),
(22, 8, 58, 5),
(23, 8, 59, 4),
(24, 8, 60, 3),
(25, 9, 29, 5),
(26, 9, 30, 4),
(27, 9, 67, 3),
(28, 10, 4, 5),
(29, 10, 8, 4),
(30, 10, 13, 3),
(31, 53, 0, 2),
(32, 53, 0, 5),
(33, 53, 0, 5),
(34, 53, 0, 5),
(35, 53, 0, 5),
(36, 53, 0, 5),
(37, 53, 0, 5),
(38, 53, 39, 5),
(39, 53, 11, 5),
(40, 53, 11, 5),
(41, 53, 12, 5),
(86, 15, 2, 5),
(87, 1, 2, 1),
(88, 2, 2, 1),
(89, 2, 2, 5),
(90, 3, 2, 3),
(91, 55, 4, 4),
(92, 55, 7, 5),
(93, 55, 3, 5),
(94, 55, 9, 5),
(95, 55, 2, 5),
(96, 55, 5, 4),
(97, 55, 1, 4),
(98, 55, 10, 2),
(99, 55, 14, 5),
(100, 55, 6, 4),
(101, 55, 8, 4);

-- --------------------------------------------------------

--
-- Table structure for table `usergenres`
--

CREATE TABLE `usergenres` (
  `user_id` int(11) NOT NULL,
  `genre_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `usergenres`
--

INSERT INTO `usergenres` (`user_id`, `genre_id`) VALUES
(1, 1),
(1, 6),
(1, 9),
(2, 3),
(2, 4),
(2, 8),
(3, 5),
(3, 7),
(3, 12),
(4, 2),
(4, 9),
(4, 14),
(5, 11),
(5, 13),
(5, 15),
(6, 1),
(6, 3),
(6, 5),
(7, 4),
(7, 8),
(7, 10),
(8, 6),
(8, 9),
(8, 12),
(9, 2),
(9, 7),
(9, 11),
(10, 5),
(10, 13),
(10, 14),
(13, 5),
(15, 1),
(16, 1),
(16, 5),
(17, 1),
(17, 5),
(21, 1),
(21, 3),
(21, 7),
(22, 3),
(23, 7),
(24, 7),
(25, 5),
(26, 5),
(28, 8),
(29, 5),
(30, 5),
(30, 8),
(31, 8),
(32, 1),
(32, 3),
(32, 5),
(32, 7),
(32, 8),
(33, 8),
(34, 8),
(35, 1),
(35, 3),
(35, 5),
(35, 7),
(35, 8),
(36, 1),
(36, 3),
(36, 5),
(36, 7),
(36, 8),
(37, 1),
(37, 3),
(37, 5),
(37, 7),
(37, 8),
(38, 1),
(38, 3),
(38, 5),
(38, 7),
(38, 8),
(39, 5),
(39, 8),
(40, 8),
(41, 1),
(41, 3),
(41, 5),
(41, 7),
(42, 1),
(42, 3),
(42, 5),
(42, 7),
(42, 8),
(43, 1),
(43, 3),
(43, 5),
(43, 7),
(43, 8),
(44, 1),
(44, 3),
(44, 5),
(44, 7),
(44, 8),
(45, 1),
(45, 5),
(45, 7),
(45, 8),
(46, 1),
(46, 3),
(46, 5),
(46, 7),
(46, 8),
(47, 1),
(47, 3),
(47, 5),
(47, 7),
(47, 8),
(48, 1),
(48, 3),
(48, 5),
(48, 7),
(48, 8),
(49, 1),
(49, 3),
(49, 5),
(49, 7),
(49, 8),
(50, 1),
(50, 3),
(50, 5),
(50, 7),
(50, 8),
(51, 1),
(51, 3),
(51, 5),
(51, 7),
(51, 8),
(52, 1),
(52, 3),
(52, 5),
(52, 7),
(52, 8),
(53, 1),
(53, 3),
(53, 5),
(53, 7),
(53, 8),
(54, 1),
(54, 3),
(54, 5),
(54, 7),
(54, 8),
(55, 1),
(55, 8);

-- --------------------------------------------------------

--
-- Table structure for table `userlistmovies`
--

CREATE TABLE `userlistmovies` (
  `list_id` int(11) NOT NULL,
  `movie_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `userlistmovies`
--

INSERT INTO `userlistmovies` (`list_id`, `movie_id`) VALUES
(1, 3),
(1, 20),
(1, 26),
(1, 27),
(1, 28),
(2, 18),
(2, 19),
(2, 64),
(2, 68),
(3, 10),
(3, 35),
(3, 37),
(3, 69),
(4, 6),
(4, 55),
(4, 56),
(4, 57),
(4, 58),
(5, 7),
(5, 25),
(5, 31),
(5, 70),
(6, 1),
(6, 5),
(6, 21),
(6, 41),
(6, 50),
(7, 16),
(7, 17),
(7, 23),
(7, 24),
(8, 26),
(8, 58),
(8, 59),
(8, 60),
(9, 29),
(9, 30),
(9, 67),
(9, 70),
(10, 4),
(10, 8),
(10, 13),
(10, 34),
(11, 1),
(11, 2),
(11, 3),
(12, 3),
(12, 9),
(18, 1),
(18, 2),
(18, 3);

-- --------------------------------------------------------

--
-- Table structure for table `userlists`
--

CREATE TABLE `userlists` (
  `list_id` int(11) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `list_name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `userlists`
--

INSERT INTO `userlists` (`list_id`, `user_id`, `list_name`) VALUES
(1, 1, 'My Favorite Sci-Fi Movies'),
(2, 2, 'Romantic Comedies I Love'),
(3, 3, 'Horror Movies to Watch'),
(4, 4, 'Epic Adventures'),
(5, 5, 'True Stories'),
(6, 6, 'Action-Packed Films'),
(7, 7, 'Touching Dramas'),
(8, 8, 'Fantasy Worlds'),
(9, 9, 'Thrilling Mysteries'),
(10, 10, 'War Epics'),
(11, 17, 'fajtalankodas'),
(12, 17, 'kutyaszarfaszgeci'),
(18, 55, 'Kedvencek');

-- --------------------------------------------------------

--
-- Table structure for table `usermovies`
--

CREATE TABLE `usermovies` (
  `user_id` int(11) NOT NULL,
  `movie_id` int(11) NOT NULL,
  `Status` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `usermovies`
--

INSERT INTO `usermovies` (`user_id`, `movie_id`, `Status`) VALUES
(1, 1, 'Watched'),
(1, 3, 'Watched'),
(1, 5, 'To-Watch'),
(2, 18, 'Watched'),
(2, 19, 'Watched'),
(2, 64, 'To-Watch'),
(3, 10, 'Watched'),
(3, 35, 'Watched'),
(3, 37, 'To-Watch'),
(4, 6, 'Watched'),
(4, 55, 'Watched'),
(4, 58, 'To-Watch'),
(5, 7, 'Watched'),
(5, 25, 'Watched'),
(5, 31, 'To-Watch'),
(6, 1, 'Watched'),
(6, 5, 'Watched'),
(6, 21, 'To-Watch'),
(7, 16, 'Watched'),
(7, 17, 'Watched'),
(7, 23, 'To-Watch'),
(8, 58, 'Watched'),
(8, 59, 'Watched'),
(8, 60, 'To-Watch'),
(9, 29, 'Watched'),
(9, 30, 'Watched'),
(9, 67, 'To-Watch'),
(10, 4, 'Watched'),
(10, 8, 'Watched'),
(10, 13, 'To-Watch'),
(17, 1, 'favorite'),
(17, 1, 'plan'),
(17, 1, 'watched'),
(17, 29, 'watched'),
(38, 1, 'favorite'),
(38, 42, 'favorite'),
(38, 44, 'favorite'),
(38, 45, 'favorite'),
(38, 46, 'favorite'),
(39, 10, 'favorite'),
(39, 19, 'favorite'),
(39, 35, 'favorite'),
(39, 37, 'favorite'),
(39, 64, 'favorite'),
(39, 68, 'favorite'),
(39, 69, 'favorite'),
(40, 19, 'favorite'),
(41, 11, 'favorite'),
(41, 14, 'favorite'),
(41, 15, 'favorite'),
(41, 18, 'favorite'),
(41, 21, 'favorite'),
(41, 29, 'favorite'),
(41, 31, 'favorite'),
(41, 32, 'favorite'),
(41, 33, 'favorite'),
(41, 34, 'favorite'),
(41, 35, 'favorite'),
(41, 36, 'favorite'),
(41, 38, 'favorite'),
(41, 39, 'favorite'),
(41, 40, 'favorite'),
(41, 41, 'favorite'),
(41, 42, 'favorite'),
(41, 43, 'favorite'),
(41, 44, 'favorite'),
(41, 46, 'favorite'),
(41, 49, 'favorite'),
(41, 51, 'favorite'),
(41, 52, 'favorite'),
(41, 54, 'favorite'),
(41, 66, 'favorite'),
(42, 1, 'favorite'),
(42, 2, 'favorite'),
(42, 5, 'favorite'),
(42, 10, 'favorite'),
(42, 11, 'favorite'),
(42, 12, 'favorite'),
(42, 14, 'favorite'),
(42, 15, 'favorite'),
(42, 18, 'favorite'),
(42, 19, 'favorite'),
(42, 21, 'favorite'),
(42, 29, 'favorite'),
(42, 31, 'favorite'),
(42, 32, 'favorite'),
(42, 33, 'favorite'),
(42, 35, 'favorite'),
(43, 2, 'favorite'),
(43, 5, 'favorite'),
(43, 12, 'favorite'),
(43, 37, 'favorite'),
(44, 1, 'favorite'),
(44, 5, 'plan'),
(45, 1, 'plan'),
(45, 2, 'watched'),
(45, 5, 'watched'),
(45, 12, 'watched'),
(45, 21, 'watched'),
(46, 2, 'favorite'),
(46, 2, 'plan'),
(46, 5, 'favorite'),
(46, 5, 'plan'),
(46, 5, 'watched'),
(47, 1, 'favorite'),
(47, 1, 'plan'),
(47, 1, 'watched'),
(47, 2, 'favorite'),
(47, 2, 'plan'),
(47, 2, 'watched'),
(47, 5, 'favorite'),
(47, 5, 'plan'),
(47, 5, 'watched'),
(47, 12, 'favorite'),
(47, 21, 'favorite'),
(47, 21, 'plan'),
(47, 21, 'watched'),
(47, 34, 'favorite'),
(47, 34, 'plan'),
(47, 34, 'watched'),
(48, 1, 'watched'),
(49, 1, 'watched'),
(49, 2, 'favorite'),
(49, 5, 'plan'),
(49, 29, 'plan'),
(50, 2, 'plan'),
(50, 2, 'watched'),
(51, 1, 'favorite'),
(51, 1, 'plan'),
(51, 1, 'watched'),
(52, 1, 'favorite'),
(52, 1, 'plan'),
(52, 1, 'watched'),
(52, 2, 'favorite'),
(52, 2, 'plan'),
(53, 1, 'favorite'),
(53, 1, 'plan'),
(53, 1, 'watched'),
(53, 2, 'favorite'),
(53, 2, 'plan'),
(53, 2, 'watched'),
(53, 5, 'favorite'),
(53, 5, 'plan'),
(53, 5, 'watched'),
(53, 29, 'favorite'),
(53, 67, 'favorite'),
(53, 116, 'favorite'),
(54, 1, 'plan'),
(54, 1, 'watched'),
(54, 2, 'favorite'),
(54, 2, 'plan'),
(54, 12, 'favorite'),
(54, 12, 'plan'),
(54, 41, 'favorite'),
(54, 41, 'plan'),
(54, 43, 'plan'),
(55, 1, 'favorite'),
(55, 1, 'watched'),
(55, 2, 'favorite'),
(55, 2, 'watched'),
(55, 5, 'plan'),
(55, 10, 'favorite'),
(55, 12, 'favorite'),
(55, 12, 'watched'),
(55, 18, 'favorite'),
(55, 18, 'watched'),
(55, 21, 'plan'),
(55, 34, 'plan'),
(55, 36, 'plan'),
(55, 44, 'watched'),
(55, 45, 'watched'),
(55, 46, 'plan');

-- --------------------------------------------------------

--
-- Table structure for table `userpersons`
--

CREATE TABLE `userpersons` (
  `user_id` int(11) NOT NULL,
  `person_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `userpersons`
--

INSERT INTO `userpersons` (`user_id`, `person_id`) VALUES
(1, 1),
(1, 11),
(1, 17),
(2, 4),
(2, 13),
(2, 19),
(3, 2),
(3, 36),
(3, 41),
(4, 5),
(4, 91),
(4, 92),
(5, 9),
(5, 11),
(5, 12),
(6, 3),
(6, 12),
(6, 41),
(7, 6),
(7, 14),
(7, 23),
(8, 7),
(8, 25),
(8, 26),
(9, 8),
(9, 38),
(9, 39),
(10, 10),
(10, 17),
(10, 24),
(29, 13),
(30, 12),
(31, 12),
(32, 12),
(32, 13),
(32, 59),
(33, 12),
(34, 12),
(35, 12),
(36, 12),
(37, 12),
(38, 33),
(39, 47),
(40, 47),
(41, 12),
(42, 12),
(43, 12),
(44, 20),
(44, 24),
(44, 28),
(44, 32),
(44, 33),
(44, 35),
(44, 40),
(44, 54),
(44, 59),
(44, 60),
(44, 64),
(44, 69),
(44, 79),
(44, 88),
(44, 89),
(44, 99),
(45, 12),
(46, 12),
(47, 33),
(48, 12),
(49, 12),
(50, 12),
(51, 12),
(52, 12),
(53, 40),
(54, 59),
(55, 51);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password_hash` blob NOT NULL,
  `password_salt` blob NOT NULL,
  `email` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `username`, `password_hash`, `password_salt`, `email`) VALUES
(1, 'moviebuff1', 0x6861736831, 0x73616c7431, 'moviebuff1@example.com'),
(2, 'cinemafan2', 0x6861736832, 0x73616c7432, 'cinemafan2@example.com'),
(3, 'filmlover3', 0x6861736833, 0x73616c7433, 'filmlover3@example.com'),
(4, 'popcorn4', 0x6861736834, 0x73616c7434, 'popcorn4@example.com'),
(5, 'screenaddict5', 0x6861736835, 0x73616c7435, 'screenaddict5@example.com'),
(6, 'moviejunkie6', 0x6861736836, 0x73616c7436, 'moviejunkie6@example.com'),
(7, 'filmcritic7', 0x6861736837, 0x73616c7437, 'filmcritic7@example.com'),
(8, 'cinemagic8', 0x6861736838, 0x73616c7438, 'cinemagic8@example.com'),
(9, 'reelenthusiast9', 0x366939656e364e306f5857335a3445634f597a496d4853356268456d4c466e39776d6b78544b344e2f4e453d, 0x63366235633634362d633661622d343464302d386537312d353238396461363463333563, 'reelenthusiast9@example.com'),
(10, 'moviemaven10', 0x566c6f687277494d66614a314838375550797a6f4f64614379366c795830792f55522f6e495830475853633d, 0x30613332653664632d353330372d346233322d616530372d646361356136313562613565, 'moviemaven10@example.com'),
(12, 'kalap', 0xb3c69fc4b6f97df291257a547b9a7179840676b48358cd493cc7e4cb97d67f505916a36df1bbdcccc0940b4bec9b52d7969ed5fef32bba7792e3b9ecae7183b8, 0x979ef2021f546cee56e0865d7cab3db5e0110071b5721fddc7a7f6d9c0e4c669bc04d234d6bc1c625f113b43bb82da43113f5172abf03cd31b6b8303a11e7b18462ef0f2429eb1cef70085f46b6f7bc50a047cf6347ab145f48d90d63bb9a585e5e6d89d8310de5479540e6d5fa8991bbd8c3d7a05baa42af72141e1b0e1f3b8, 'kabat@gmail.com'),
(17, 'norbi', 0xed6b2367c17523df9207584b8e7d6a2e6d40ae2931799ccfbf9fe57e641107b2ed9ce19eb80ec180f7ec453fd332b6d1a855aedcb5ed1df8f4443e4833d50dde, 0x16027c91ba557528e78d5d9d509f95bc5fc6efbf4df3baf94e81ee40f5989f96f85c17e349ac1b0169ada24c3b6770ae75062235d32acb170be59da94a0741c3e049ebb30ca25e40f155158cacc149cd639f995030d3d13c9a40ea398e1819ed89daefcaaf1b035ef0c8ef0f7207c66ceb236cc1e34cc5ea633402e3c0a220db, 'norbi@gmail.com'),
(18, 'tomika', 0xf318f7eb6f7aea3ded13c1a0274f6722187482f5e4b05718bd694883cd3a8a91071f15b8dffe1145daf7a5ee91978b275f79f81b68dad201b8354ec519d2a8c3, 0x775971f664d45520e834b01a7a5a39db7c1eb3d3fdb77906ccdd47551188a5c42e3cedb8438a73991308bf53c619705e200838a508aa4594b1d0f557a13cea2573901b54d6e5992992d881034670415801d5919e6b9cf55f8546332bf74fde33a91075a54e1112a4f31298d317e3caba4fc7a8fc0c3e52925010804c6ca9b47b, 'tomika@fos.hu'),
(19, 'tomika123', 0xa755b2cbe5fcf9262754004bfc62a21d1d4d4de2dd2f2105e1c80ef2919b1630a321df6dd540075991e601a7c3cd3c623a43ab8fcd5856b11a16e88c2552b805, 0xc58ef8d5dd602971b014a03470bbb135e18ef3c2b1cdb7fd0bb6d1906fe2c01f1ff3574c36b13850069a3bbab0ac3ab9468013ebc46b306bf57330bc1f746638140daa879a7c198202e508b228bb5d06702e83767eb06736c6a503091e52e6ce8b1a1d20b28fb1a8fb5a21bf57aad9093c4fb9672fdd81b47345a14757bfb48c, 'was@fos'),
(22, 'comedy', 0x86991b991f6b7a8c0facbca9d5b078af6faa7b42864c9b41e5e8a31ebecbfaf07cd19887b44aff1464889b0d331ec7ddbc3cba6050c471d7b4830e67170d22e5, 0x05ed31596e06a95da876241b7c293854ee404226c228a2e51cc82bbb075c9bea1ea04d6d73fb0df834f96d5c851606f28e778d8d472f8bc4e3929d0468748120db4fa87c980180bc89bd5ca3bc1bf8ca8d8f41e185ed0d18c49065934443cddd5c7d39976f5a7a023f19bae7967307d35f98efe92cd4311d441da6098e1d6797, 'comedy@gmail.com'),
(24, 'kalapkabat', 0x1344092eb504e1d31d696167eb3616125ab7172432a17ecdf43179f44673ead214fa74492ac2a756f0a96c72a0c8c0af9e5546955055ee3840c3d9dc4487a58d, 0x71d66f47a9cbd82dea4f7076087ac201b641e4b8f3787d9798e11789bda39449f637346d79cd84fa74a1cef7cf2af4936d1b69bea94bbd926a670b38fa7e0db3e4bd6eb7a3c601b3bbc1451146fa103c63eafe15de97564c877ad72b6c051f2c685b36dec08427065a8dcdd1c7bb72e1a73357bb3c5c1005734716101bbea7e5, 'kalapkabat@gmail.com'),
(26, 'barbihorror', 0xc140c0414c2e465d43898111004a787f9aa02b40a776986e89eb362525bb014362516dd0c2b6b04a659771a7ee2a0c7cb5dd001f80f1344988e9b49107e0b8a2, 0xca14645cfd1739939956146818961b46c7ec7a863c3e007a3e3384e07d5ea13111c150d96315d61dfa9c67edadc0c594c364e41a3c0af2493246b0949dce173f5e3001d764923d7b426f11b7047c2a017069f33a82a3a92dcb46da864cc38c8f89ff1b7492509e191a43ec543cf42083e8f739baa4aa0d165578158cecf4c5c6, 'barbihorror@gmail.com'),
(27, 'yxc', 0xa3a1dc5d556ee94f9223271427b24fd7ffcff879a58b504d017007750f7eaa3f3e577a93c16ee1c84482c7fb09007e2c67c5d8d683416a0cb4638cd0c42f20b0, 0xfb4b196ab0b7a44785a742b76f60af0a73a0f46d65868e2a482d10b772ed33bb93ce626b8c9a47c59f8732d3bd7da25997a13695849d67729a5cd2a9cc477dbb033b6048ef849cf24f50021e43a895e50711f3041a30303ae58653b95a09454ef22a66b45b9bf24d482fb61293aa0b10c6388b658752ea42fb8ce5f5c31015fb, 'yxc@l'),
(28, 'op', 0x2e8f84050fe01e56c87eec29922fde8257bc993067f9e86ffcb12fbc1b667b80324644339e723d4c9036409313555d087bcc003817de12d4ee356d6793e79219, 0xf38cf048a431ecd869c490ef12bc44e5d4330a5f93c02f13b6d31ccdc02297b1d10ef8efabdddc166cfbba46ef571fe7e8743a0e933901d01e551b310942e2d9cad8ced808d2cefb9f59c2aad0de93c19bf92bdc0c4ded59eb3b1b89f112ffefa8571b44fc2088f0ef008fcc4ac7ffea70759f3e41718c708e33f1c564d10272, 'op@l'),
(29, 'test1', 0xa70d4eb99cd33abbe94bc4eb99d043cddc83e740827bc3d0170ba84b23990f12633c9726a05dfa35a5887e77bf85d50f285b29110dd59869280f387576df03e8, 0x0c266780dd12e0d03bf7df15bfb3a6726c0399d4546ffb488d4b59e98a23ff0036f1a93a48024236fa7caaff34d2096d0adf9503807e7286e238bc9aebd758152a0eb9e3637ec5b5ff7d519f6318fe238891a3cd81fc220f063fa2face596980ebb8453ee48faf3b6f097508e253dd2fa3fa267f583e14f7b8f33bcae4fb6a44, 'test1@gmail.com'),
(30, 'test2', 0x7d38087bca69ceb4a57d95a8a2e5fec73ba7d6253f0107d9d563af759a33f8c2fa3c1880e1c8f5928d863e673b77a4d25264e248f42fd7a5b9122a113d0bb8f9, 0x1e2848f8f69c3bce39d94e12b8a30920c22c6b339bc47559f26024bafb1f87cacb2db3339573afe26b7c60cbbc48546a1295806bada679fae0088098fa127ba7f221d90741c8297fb141c424248162b9e5d39e1002364307bf029d4af9a950cde5e82755810d769931ee3fe438f410423a15bbd35d4bee1dc0623089004f18ca, 'test2@gmail.com'),
(31, 'test3', 0x614193e0cab81484b101a060abfb64924390336a59b614d5b481cd9d122b90acf6dd8178726d5bff2e1b61f475e0a51585533a048f9f77f34973d37d27c9a84b, 0x7663ce375865e65bdc021ff46eb3be76cd85a921f60507ee814549e2f6d3cebd79ded86123b75f62238b79062ff51755e3e45671a53a2ff845271130d32c1cff73de2d0a5ae02a6e446604f586a3d5ce6163209bffedeb9a8f805b7252f8d291cee303d03f621061a5e76ab50556caeede012425b9c782882c8986d231f38fdf, 'test3@gmail.com'),
(32, 'test4@gmail.com', 0x00400dc93c54fcaf78274f1b6d6c55c9f1cb5e3d2df92b6ad6ca8fa5873c63069bf7049005bde60ba974be21c965e9a2bc2b89a4933de22d1a8cea4cc24fe552, 0x407a22491b842af895a37fdf1b3af06e19e34cf23ce75bf9c16d1224d55d9da613fa7bdf719ac6a4df0bd2768f8dbb53f45a66f7917543b1ec8497e4132aaebb1eaec2e5136d001554c89dacaf95e454cb4bdf7ea7512e9b820adb36192339f580e5cf67aa05becb728923230f72b58349e34034962ffb7617450e6dc8d7ce11, 'test4@gmail.com'),
(33, 'test5', 0x4bae88ed568be495c98d83b6ad774940114130fcc07001744e6e5552e21d2ed252ef3990c139a0f3104c91368537b72f2d76e4fe7aa112149e26fb142ee7a3ed, 0x73df5c2a2c98d3a51f937b51f90a5ab7c2ec107eccfbc09a83bc3d64655a132b39936a048ab3dd4d91ee68ac8e3405164e7b3f5b67ea66161ba17d56c577a108adf0fc3d26709c1dad755c912260fc0194aa0be6a95b8a48ddc5743342a1d4476091487abefdf2db6638589c5684ec02c0ce7421422f83af6ff0c163e5d8649b, 'test5@gmail.com'),
(34, 'n', 0xabc70bbb0366ca6a756b95f593329077bf92915851d12cc1763eb8e05f47394b7376e4d387d135ec0cbe38450fdc718965f43f5e475260cf4dda99a1d28b9af0, 0xc28aed929115145bc1b00f675c580d75d83653248373fd4e0966c7c62501d2dc8eb46422742d7c3903c103db8568587e2c360b7cb59326c564164cff92c9fd7e006d8b5f06a2c02bcd565dbcbe5a92f8ec340c3f88b925965707efdbbe70cd92895ce38ea019b1a90b6816087c3500475f2515ecad637ba538ce5c184271d374, 'n@n'),
(35, 'teszt', 0x74d03fa6f2d4b057d306d6021319cf1210f27545d51973ce2133fd0477e153dae451b5d3b683fe5171a678120954e0b992a52249cd315d2f17b29024231e115d, 0x998cf7be54782809e14bc7e11500b18c103711202db99bbceead310c01f28e1040fbb75865cf4ac999fa4aa06c97be23cd7cfd17d6a2763b9648374abe6fa68f6c62a4ade315ff1365d9c34fe18172eaac3868f911f0c378e7a8a1ac1b5ffb4bbdcd491fa38a9375b5691512cc4bddf5ce9bd6c75224a1295226cfcd149b8dad, 'teszt@gmail.com'),
(36, 'teszt1', 0x5ce9f62e154c19603de53d65ba99956878cbc334156e83c2bcc520d22f2e7bbab9743362a7b5a9bb84a7f820ebe2ccf0449e396f76b121303a26d96eeee150c0, 0x33337e7cc4f90c5ef2e6d23b05480bafd891de513096e283e2c089b0d2a7c2027e60ad4a8ad42b38bc9a1fc721117c9f31827f679050a125c09ba4cb8d85cf9a7c8d91705989851252d17e3dc3b753d30ae07f0c15bde50f2bac1224ad8c9f983d8a0e7908675de1f9c1833c6c80941b6127a449bd5210be807f17ee84c3c183, 'teszt1@gmail.com'),
(37, 'uj', 0x63f88793741bbe64086dfe42ff65fd85eac5c2ce47dc64abc0da568f865a85bb147a2743f9e8600f7ce517d13af05c892b2aace4a206d44e4618da84cf238dd7, 0xbee494840e2c00daea4538ad5c0f2138f93aa5cf947b2de8e20e889d9946b64fb71d2a2f9e3273c71401c7f7b64d88d528280695a15c7b4e5051808766ce1e6119bdebff432c7b93b9e79e883db657b44b782061156189cbea2fd9408d799998f064df51233a702c6027156d853476b82ec65acc7e9e7332df4e671aa54c09f5, 'uj@gmail.com'),
(38, 'r', 0x8c4644cdf0ec1eabeb31c19d031d4377d50653d042750c94cfca3c64d1895ad052e57c137ecbd49ab739592b9335bb77e35a29e4609c14b9c07cfb1fc27aa9ea, 0x5b874a132268ce83ccfa1990ebdf9e8419202bddc6334fc615337fd6352f2285d55457f4719cec44cf5888aea83d6e347a09d75c1834aa6da318dddc42d6d4f738a2e76d02055f0d76c7a90f2a22523f2a421647a388b5a4ac79a3330065cfa6f604d80121b7a1dd71e1ecc1e4c2e93181d507b0bc4c2bb5e9aaeebac4644bb3, 'r@gmail.com'),
(39, 'nikol', 0xf1af572ce5e56beb42e6d0e2b2e256cf3c66e8296c871f617a98b2103d37da27cc4c24358f60f791a27cf28ff387b606043e6125cacd91f0513747fdc3b2a90c, 0xcc8376c98a08700dd04f224fd8924ce8f844fcb587c5549c8ad64eb5aff2a3524058fa2b1688abda45b4573f44f01f3ef34205c35486aa948fe75e7494b3098f7fa075b14551c4805bad264f68b7452b0337d5ddddaa817876e40219cdf5b6dee37460e75c051e01d6f30cd4c26c48a818afba5a995b151d145f673239f7747f, 'm.nikol@gmail.com'),
(40, 'bella', 0x975cf27889b03ddb6f50b0ae15cccb2f217bae904e1b7bf4e4b574666ac3c876314c28f35eb78cb240ea59ca3ffecc224ca7967306f7be5840bf999f94a20708, 0x4a1770545715eb01066319a13e58a2f750b6b05ab6aab8e6b66af64c693e0880cb41ad2b26e964a2af37c2b904ade01fc22c680099f1273a92ced992e0f12c53df4cd94675786f0c57012a4308c65fd53223c9f2771521a1f072da9dbd66f0c54cd176cb82f7d7020f2459eb287944ffa363450ccfb6e5a72ab37f23fe4457bd, 'bella@gmail.com'),
(41, 'lepes', 0x94c871278d6b346de147dc320b2f743da9b97fe080f50344a44871ad16d2046ebead87ba42a1fe4d0bc6d342da3de404259a8026d07d5b39fc76cad9979b3856, 0x4e770fc0087a10624fc93346a0270e7dbb722df40091e5785ef64724759eed4b507023bc0e241f54c0f92dd4bfa28e2bbbaf93880b30e6fa4f189787424bdd185e0c3b9ae9992ee896b2001f7bea41ba52efb7fc158a3fc4451e38da106ac3d88c2a14ee2c06ff84a7c4ec1d48ca834a0bb1f8cde75dcc71deb9e6dabcf01f66, 'lepes@gmail.com'),
(42, 'nezd', 0x6a3e4160a3f2e6c6db1a216071203912da5775881b94623fbf33c278bf2c3be898f60bdf806dbe04a807d38a2226465bab19abf153f2bb584b6461ba4304977e, 0x4b6a10f18666d2940d9929b0ac6371db5bc55c40f4fc27d766dcac342b9bddfae477b4326b6b30ef069565cd77df64f555de6142e15c23a58113327fa0d38ef418700dee29aba93e28a5048cdc9eeb4dd3cf6819573cef2dd948da66edba735f7bbcafae8645b3e5406981530e855f3b080ccf85de429f825ea8aa398abd2f35, 'nezd@gmail.com'),
(43, 'da', 0xcf3e8b5d633f9f996f2f087595af5028aa7432645f81342b185381f4a5bf336f2c3969773f982e2f1966070107da6dc4e8e33a6a13616e153fff815fae4249d9, 0x1c4a57d7f71f4b5870b482925459b682f9873cb5cdf4f3083112a484d5fdd40a24c2725e38ec430782245761b3c33e9e56d42dc237bfcbfc2c96fdfbacc7075a5a867aa60dd140361fe2d2f16d459500f9b12616b065aa3adf53d567640d33e896d0228ae2686317b8decbc1bdb3a8261a28dbb67e4153506cf4a7281adcc543, 'da@gmail.com'),
(44, 'sok', 0xd5832b4e592e70c659a014783c744f0ee0efad5778d8fc953c49711ae793c5e1e47b04f3ac9321472274ed2562d087e0662a299a83fac7cab51bb30ab7c66dd5, 0xb6d9a0998413c425c2899bba52c4e9dd46e39365fe5343cb7dbebded34f6e7c812a73a59a9f91ed107674eca1cf2eeeb6e5670dc258dd4e630d9be536335f156122996ddb0c799ed6372f6a37f079f399d745f348b34f94b1662333b462c6f8450c61c722cf21ab6b6d59006c1025c85f7cf9f470df6d91b26a8db5b282e770e, 'sok@gmail.com'),
(45, 'norbert', 0x799e747e1d5f819fc2caaa5e581a3563e814f7526844183d723256bad0222cb5a44da29c0f0f7f17898f646f0d53a3f32c5c73e3ae3b459510cd849f6a1356f3, 0xed14821b557a9ab2b1fe752462896748186b0472fe940b7a9b0fe791ae2904d36d380d8288f8f3ca1d5906e184e31d0244ae1268841ddbab1fc4eeed9a6976fae7ff5720a0ab4029c48c347f5398a7ae128f6e2469971321b36afb9ac306a5497a769c895463fd1896d5baf0d4c597f641fa73c55d21dbbea297f0d8e453914e, 'norbert@gmail.com'),
(46, 'app', 0xea5b308edfb2fe6b6e7ad1f0f93eec58ec960e1a03e23bba6cdc3ee6329d2a442bc1b49ff5335eedeedcedc0298e53ea48286eff7c168cc1c022a610f93dd304, 0x65f7f9b01f27001767040f3dba3cd4aeaef0c86daaac7069afecca754291e4b12f2031a4464875ab8d626ae0ee24cb8bb8c7584aef1a03129b347a9a48033749caa8720f4a7afebc76b6ba7c5161d539b3ed41aaf0db4b796c53044a4a7c677c91d53ee19b8ea08eb0d9f1ce0e13a90f781e8e9457926237ae80637d30a8c6e5, 'ap@gmail.com'),
(47, 'profil', 0xcbd26f5a8311aed84c64d8775466488b8e830d21b82b00a179b7d7bf6e38ac07865e2271bf5ee2fc8d0e899344e52ed48d9a7f4d52058e40f1ab3867b5674a39, 0xfd99eb6d6a4b178a567361ed10e6314fd5cf637bc00adcdbeecdce29221ba72069c2c88d92761180ef386896670bb8896ac08cb411faf74ee69288502018aabc03b5c06f2b64bf8fc43611712894aceb37aaac951ef8d74ad67c4de9fd17a7e5b01029b82b7249ae49b3a26e35c916bc2839490ccf4f20592968010b8118c159, 'profil@gmail.com'),
(48, 'd', 0x552dc33c5d2bb590fd27ec637dd3812710dfc5ec6217ece48ed08897bc7e1c908932bcc8bd20050540d20c708b132d5b18ca6cf3bed30d7a9b126a6a122b4cc8, 0x99cf64498aa5fa81934df697476b928ee00bcc013388b7ece8b4226003aa7e32f3752cfd65bdfd0eb348741f0340c69e0d6dadc52fb12f20a114b35642eb6d2c2e65f244064e249bb331c0e70183f4d084fa934b53eeb96c9227f1a425b80f867fc5a52d7af0c48cec56361ed76a16c8e1c04f595dd379ae91ebc790cd5f96fd, 'd@gmail.com'),
(49, 'projektparty', 0x8ef46c6b2d5b6ee54e4276c2430173a9c71b10d9c75f42f0d93017221587de55791a0d937f20569a170938227970ebc8568f635d3bf51b15a88b4bfb081adb3b, 0x16bb815509aaf3670545b150359eef254feb17c7bc38d7d6c942e12d3408d2e75e1a32d0b9c95346c766b9c0ac4e476865b719a1813b65b1ff744af6d3fb16635c7636eee0cc2452a5058cbd51c4930a65e1530d4ae49ed075c75dc35c6af0d8b46142c12d7850fdf0b1b7022eacfddb19ce46068b8e03ba59b9619232ee52bc, 'party@gmail.com'),
(50, 'husvet', 0x1786114c70bd631f3e94cb5a0981200fac62b830e0a76a56a48c68d0a4b0aa3442c95e5cfa89cb758793f9ed928cb753da72405025556d3ddf6ee9e14e344124, 0x9a4228b7792958fbad42edc784bfc2e2c8e75c4bd623af8af84d95e14d0fd7f8678e378bac79e55696c9908a9567de0d965ae445fd8414801684521dab7cf4c8c4815066f9ad7c9cc9eb46c584bb8e833886622f91b9edd9a7ab84c1322c0218697330619386c71f3993e7e5a10ad887385dcf261d6002e327bb05e5b8cab1c8, 'husvet@gmail.com'),
(51, 'mag', 0x3dec36fc7f93841522179d032e91b6997de36f351fd879cc5c636054a985147781f75291b9e1ffaecc5220b58e2d7564d943b2558ac1593636708802cf0060e1, 0xa63e8fc3a629cf823a91e33811d47c1f3ef2c9d6610be28f83ec15499a0ea80f3ab20796c23fe1244941301cba5fee337b44c458154397e9a09a26e4f2cc487087017fdf0bf551b84cfbd3903b2ae83ebe34ae087c0c00df050fc2a7eb208be47b0184c965ca2e5957d142aec0196efc1c6cddd675c633e356c4781dc59a1b12, 'mag@gmail.com'),
(52, 'margo', 0x3c88203d6f098a86b2e100c43bc05bb69e66f3c954e87bce945f6acb6a0ad73289139232da81526eee34f9b55b418907e1c954a22e5e4f5f46be914b7c4dc4e0, 0x7f2f71117eac3aac41cc03ce7b12ed2a530ec4c18846cfad1c29a20313dde91d2d80f53e51bf6915a490503b5072419588f478138f19bd579b1c46f697a1d502f92d8aa089740b961001bd9476e008e81dc20fee44a0786cd0e256f74b740b5737a2a61fcb2f051c41a54f4bb910d30357fa384f2da86367619616563f7907d1, 'margo@gmail.com'),
(53, 'p', 0xe64f6f73de43d36d990a1ca635a7ad35b78bf734ee9ce704ca5e135a896db7e14e50b1385a0f97a224379969abdb4d07378373fb7ead5713e37e61170f172e1d, 0xdd085cf50dce21d8169479da4c44f1eddc7c26c2a87a5d7e0fef45ef3169f4f35022af008367600da70d791006a3bf14e36e8b19dfd26826a26b07fd635ba854e1908c6f057b939ccfe47276095b0f6dd216b971c19e72a1b8d400b2144c4102c0639e8fbcb5204ad9b9929fd43a21a620045a84ddc1c250046707576bb0d1a2, 'p@gmail.com'),
(54, 'jaj', 0x76302367bded4c005e5e4b1453310bdb0e67f851e6468c3b0fb60350d2b675fc965aaeae209fe1faa8e1bc94635cffcf6faa83812b9bde76a06bed9f30c442b9, 0xa7464b50f6f66e32ca1045ee5bab07990ad262f6f40cfa1a7a22e417185c65f237eaedc6e5a96da7556fb900e930037a5c302c5f2c393fa0061feb6453a3956142a54991e1608cb67325ff827d131bddb6528e603e77e30933d0c5060aa58ded073b5012d047feb4cbb5eed9aad281049a8aed728e28990ff63feb839d52bc81, 'jaj@gmail.com'),
(55, 'Kovács Péter', 0xdb82335116c52cc710e23495090d5c0c0d6b7ca93be7f6d0b24024b227e85e215ebb22148016d986e520d8d4d151cb28f34115bd7e3b134d989f2291b33847c9, 0x463dee7de53bf3a256acf122ad90f4c00e3edfe803e57cce80a975daeb21d7d7888e9f07af8700d8ed41544a153018227d33e94eca8f7517cddfb49c9b62e5ca5f6bf103406ca796ef4cddbee201626b25fbe77bd2d86866dbb061564fb293976db5ccfbc0d394a48e09ec652a2a6af503314ffc65d0c3032f63dde0ce71c911, 'v@v.com');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `comments`
--
ALTER TABLE `comments`
  ADD PRIMARY KEY (`comment_id`),
  ADD KEY `user_id` (`user_id`),
  ADD KEY `movie_id` (`movie_id`);

--
-- Indexes for table `genres`
--
ALTER TABLE `genres`
  ADD PRIMARY KEY (`genre_id`),
  ADD UNIQUE KEY `genre_name` (`genre_name`);

--
-- Indexes for table `movieactors`
--
ALTER TABLE `movieactors`
  ADD PRIMARY KEY (`movie_id`,`person_id`),
  ADD KEY `person_id` (`person_id`);

--
-- Indexes for table `movies`
--
ALTER TABLE `movies`
  ADD PRIMARY KEY (`movie_id`),
  ADD KEY `genre_id` (`genre_id`),
  ADD KEY `director_id` (`director_id`);

--
-- Indexes for table `persons`
--
ALTER TABLE `persons`
  ADD PRIMARY KEY (`person_id`),
  ADD UNIQUE KEY `name` (`name`);

--
-- Indexes for table `ratings`
--
ALTER TABLE `ratings`
  ADD PRIMARY KEY (`rating_id`),
  ADD KEY `user_id` (`user_id`),
  ADD KEY `movie_id` (`movie_id`);

--
-- Indexes for table `usergenres`
--
ALTER TABLE `usergenres`
  ADD PRIMARY KEY (`user_id`,`genre_id`),
  ADD KEY `genre_id` (`genre_id`);

--
-- Indexes for table `userlistmovies`
--
ALTER TABLE `userlistmovies`
  ADD PRIMARY KEY (`list_id`,`movie_id`),
  ADD KEY `movie_id` (`movie_id`);

--
-- Indexes for table `userlists`
--
ALTER TABLE `userlists`
  ADD PRIMARY KEY (`list_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `usermovies`
--
ALTER TABLE `usermovies`
  ADD PRIMARY KEY (`user_id`,`movie_id`,`Status`),
  ADD KEY `movie_id` (`movie_id`);

--
-- Indexes for table `userpersons`
--
ALTER TABLE `userpersons`
  ADD PRIMARY KEY (`user_id`,`person_id`),
  ADD KEY `person_id` (`person_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `username` (`username`),
  ADD UNIQUE KEY `email` (`email`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `comments`
--
ALTER TABLE `comments`
  MODIFY `comment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=40;

--
-- AUTO_INCREMENT for table `genres`
--
ALTER TABLE `genres`
  MODIFY `genre_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT for table `movies`
--
ALTER TABLE `movies`
  MODIFY `movie_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=124;

--
-- AUTO_INCREMENT for table `persons`
--
ALTER TABLE `persons`
  MODIFY `person_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=175;

--
-- AUTO_INCREMENT for table `ratings`
--
ALTER TABLE `ratings`
  MODIFY `rating_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=102;

--
-- AUTO_INCREMENT for table `userlists`
--
ALTER TABLE `userlists`
  MODIFY `list_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=56;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `comments`
--
ALTER TABLE `comments`
  ADD CONSTRAINT `comments_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `comments_ibfk_2` FOREIGN KEY (`movie_id`) REFERENCES `movies` (`movie_id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
