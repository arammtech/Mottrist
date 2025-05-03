using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Mottrist.Repository.Migrations
{
    /// <inheritdoc />
    public partial class addCitiesSeeData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Geography",
                table: "Cities",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "New York" },
                    { 2, 1, "Los Angeles" },
                    { 3, 1, "Chicago" },
                    { 4, 1, "Houston" },
                    { 5, 1, "Phoenix" },
                    { 6, 1, "Philadelphia" },
                    { 7, 1, "San Antonio" },
                    { 8, 1, "San Diego" },
                    { 9, 1, "Dallas" },
                    { 10, 1, "San Jose" },
                    { 11, 2, "Toronto" },
                    { 12, 2, "Vancouver" },
                    { 13, 2, "Montreal" },
                    { 14, 2, "Calgary" },
                    { 15, 2, "Edmonton" },
                    { 16, 2, "Ottawa" },
                    { 17, 2, "Winnipeg" },
                    { 18, 2, "Quebec City" },
                    { 19, 2, "Hamilton" },
                    { 20, 2, "Victoria" },
                    { 21, 3, "Mexico City" },
                    { 22, 3, "Guadalajara" },
                    { 23, 3, "Monterrey" },
                    { 24, 3, "Puebla" },
                    { 25, 3, "Tijuana" },
                    { 26, 3, "Leon" },
                    { 27, 3, "Juarez" },
                    { 28, 3, "Merida" },
                    { 29, 3, "Queretaro" },
                    { 30, 3, "Cancun" },
                    { 31, 4, "Sao Paulo" },
                    { 32, 4, "Rio de Janeiro" },
                    { 33, 4, "Brasilia" },
                    { 34, 4, "Salvador" },
                    { 35, 4, "Fortaleza" },
                    { 36, 4, "Belo Horizonte" },
                    { 37, 4, "Manaus" },
                    { 38, 4, "Curitiba" },
                    { 39, 4, "Recife" },
                    { 40, 4, "Porto Alegre" },
                    { 41, 5, "Buenos Aires" },
                    { 42, 5, "Cordoba" },
                    { 43, 5, "Rosario" },
                    { 44, 5, "Mendoza" },
                    { 45, 5, "La Plata" },
                    { 46, 5, "San Miguel de Tucuman" },
                    { 47, 5, "Mar del Plata" },
                    { 48, 5, "Salta" },
                    { 49, 5, "Santa Fe" },
                    { 50, 5, "Neuquen" },
                    { 51, 6, "London" },
                    { 52, 6, "Manchester" },
                    { 53, 6, "Birmingham" },
                    { 54, 6, "Liverpool" },
                    { 55, 6, "Glasgow" },
                    { 56, 6, "Edinburgh" },
                    { 57, 6, "Bristol" },
                    { 58, 6, "Leeds" },
                    { 59, 6, "Cardiff" },
                    { 60, 6, "Newcastle" },
                    { 61, 7, "Paris" },
                    { 62, 7, "Lyon" },
                    { 63, 7, "Marseille" },
                    { 64, 7, "Toulouse" },
                    { 65, 7, "Nice" },
                    { 66, 7, "Nantes" },
                    { 67, 7, "Strasbourg" },
                    { 68, 7, "Bordeaux" },
                    { 69, 7, "Lille" },
                    { 70, 7, "Rennes" },
                    { 71, 8, "Berlin" },
                    { 72, 8, "Hamburg" },
                    { 73, 8, "Mun him" },
                    { 74, 8, "Cologne" },
                    { 75, 8, "Frankfurt" },
                    { 76, 8, "Stuttgart" },
                    { 77, 8, "Dusseldorf" },
                    { 78, 8, "Dortmund" },
                    { 79, 8, "Leipzig" },
                    { 80, 8, "Bremen" },
                    { 81, 9, "Rome" },
                    { 82, 9, "Milan" },
                    { 83, 9, "Naples" },
                    { 84, 9, "Turin" },
                    { 85, 9, "Palermo" },
                    { 86, 9, "Genoa" },
                    { 87, 9, "Bologna" },
                    { 88, 9, "Florence" },
                    { 89, 9, "Bari" },
                    { 90, 9, "Venice" },
                    { 91, 10, "Madrid" },
                    { 92, 10, "Barcelona" },
                    { 93, 10, "Valencia" },
                    { 94, 10, "Seville" },
                    { 95, 10, "Zaragoza" },
                    { 96, 10, "Malaga" },
                    { 97, 10, "Bilbao" },
                    { 98, 10, "Alicante" },
                    { 99, 10, "Cordoba" },
                    { 100, 10, "Valladolid" },
                    { 101, 11, "Shanghai" },
                    { 102, 11, "Beijing" },
                    { 103, 11, "Guangzhou" },
                    { 104, 11, "Shenzhen" },
                    { 105, 11, "Chengdu" },
                    { 106, 11, "Chongqing" },
                    { 107, 11, "Tianjin" },
                    { 108, 11, "Wuhan" },
                    { 109, 11, "Xi'an" },
                    { 110, 11, "Hangzhou" },
                    { 111, 12, "Tokyo" },
                    { 112, 12, "Osaka" },
                    { 113, 12, "Nagoya" },
                    { 114, 12, "Sapporo" },
                    { 115, 12, "Fukuoka" },
                    { 116, 12, "Kyoto" },
                    { 117, 12, "Hiroshima" },
                    { 118, 12, "Sendai" },
                    { 119, 12, "Kobe" },
                    { 120, 12, "Yokohama" },
                    { 121, 13, "Mumbai" },
                    { 122, 13, "Delhi" },
                    { 123, 13, "Bangalore" },
                    { 124, 13, "Hyderabad" },
                    { 125, 13, "Ahmedabad" },
                    { 126, 13, "Chennai" },
                    { 127, 13, "Kolkata" },
                    { 128, 13, "Pune" },
                    { 129, 13, "Jaipur" },
                    { 130, 13, "Lucknow" },
                    { 131, 14, "Seoul" },
                    { 132, 14, "Busan" },
                    { 133, 14, "Incheon" },
                    { 134, 14, "Daegu" },
                    { 135, 14, "Daejeon" },
                    { 136, 14, "Gwangju" },
                    { 137, 14, "Suwon" },
                    { 138, 14, "Ulsan" },
                    { 139, 14, "Changwon" },
                    { 140, 14, "Jeonju" },
                    { 141, 15, "Jakarta" },
                    { 142, 15, "Surabaya" },
                    { 143, 15, "Bandung" },
                    { 144, 15, "Medan" },
                    { 145, 15, "Semarang" },
                    { 146, 15, "Makassar" },
                    { 147, 15, "Palembang" },
                    { 148, 15, "Yogyakarta" },
                    { 149, 15, "Denpasar" },
                    { 150, 15, "Balikpapan" },
                    { 151, 16, "Moscow" },
                    { 152, 16, "Saint Petersburg" },
                    { 153, 16, "Novosibirsk" },
                    { 154, 16, "Yekaterinburg" },
                    { 155, 16, "Kazan" },
                    { 156, 16, "Nizhny Novgorod" },
                    { 157, 16, "Chelyabinsk" },
                    { 158, 16, "Samara" },
                    { 159, 16, "Omsk" },
                    { 160, 16, "Rostov-on-Don" },
                    { 161, 17, "Johannesburg" },
                    { 162, 17, "Cape Town" },
                    { 163, 17, "Durban" },
                    { 164, 17, "Pretoria" },
                    { 165, 17, "Port Elizabeth" },
                    { 166, 17, "Bloemfontein" },
                    { 167, 17, "East London" },
                    { 168, 17, "Polokwane" },
                    { 169, 17, "Nelspruit" },
                    { 170, 17, "Rustenburg" },
                    { 171, 18, "Cairo" },
                    { 172, 18, "Alexandria" },
                    { 173, 18, "Giza" },
                    { 174, 18, "Luxor" },
                    { 175, 18, "Aswan" },
                    { 176, 18, "Sharm El Sheikh" },
                    { 177, 18, "Hurghada" },
                    { 178, 18, "Mansoura" },
                    { 179, 18, "Tanta" },
                    { 180, 18, "Suez" },
                    { 181, 19, "Lagos" },
                    { 182, 19, "Abuja" },
                    { 183, 19, "Kano" },
                    { 184, 19, "Ibadan" },
                    { 185, 19, "Kaduna" },
                    { 186, 19, "Port Harcourt" },
                    { 187, 19, "Benin City" },
                    { 188, 19, "Maiduguri" },
                    { 189, 19, "Zaria" },
                    { 190, 19, "Jos" },
                    { 191, 20, "Nairobi" },
                    { 192, 20, "Mombasa" },
                    { 193, 20, "Kisumu" },
                    { 194, 20, "Nakuru" },
                    { 195, 20, "Eldoret" },
                    { 196, 20, "Thika" },
                    { 197, 20, "Malindi" },
                    { 198, 20, "Kitale" },
                    { 199, 20, "Garissa" },
                    { 200, 20, "Kakamega" },
                    { 201, 21, "Sydney" },
                    { 202, 21, "Melbourne" },
                    { 203, 21, "Brisbane" },
                    { 204, 21, "Perth" },
                    { 205, 21, "Adelaide" },
                    { 206, 21, "Canberra" },
                    { 207, 21, "Hobart" },
                    { 208, 21, "Darwin" },
                    { 209, 21, "Gold Coast" },
                    { 210, 21, "Newcastle" },
                    { 211, 22, "Auckland" },
                    { 212, 22, "Wellington" },
                    { 213, 22, "Christchurch" },
                    { 214, 22, "Hamilton" },
                    { 215, 22, "Tauranga" },
                    { 216, 22, "Dunedin" },
                    { 217, 22, "Palmerston North" },
                    { 218, 22, "Napier" },
                    { 219, 22, "Nelson" },
                    { 220, 22, "Rotorua" },
                    { 221, 23, "Riyadh" },
                    { 222, 23, "Jeddah" },
                    { 223, 23, "Mecca" },
                    { 224, 23, "Medina" },
                    { 225, 23, "Dammam" },
                    { 226, 23, "Khobar" },
                    { 227, 23, "Taif" },
                    { 228, 23, "Abha" },
                    { 229, 23, "Tabuk" },
                    { 230, 23, "Hail" },
                    { 231, 24, "Istanbul" },
                    { 232, 24, "Ankara" },
                    { 233, 24, "Izmir" },
                    { 234, 24, "Bursa" },
                    { 235, 24, "Adana" },
                    { 236, 24, "Antalya" },
                    { 237, 24, "Konya" },
                    { 238, 24, "Gaziantep" },
                    { 239, 24, "Sanliurfa" },
                    { 240, 24, "Mersin" },
                    { 241, 25, "Karachi" },
                    { 242, 25, "Lahore" },
                    { 243, 25, "Faisalabad" },
                    { 244, 25, "Rawalpindi" },
                    { 245, 25, "Multan" },
                    { 246, 25, "Hyderabad" },
                    { 247, 25, "Gujranwala" },
                    { 248, 25, "Peshawar" },
                    { 249, 25, "Quetta" },
                    { 250, 25, "Islamabad" },
                    { 251, 26, "Dhaka" },
                    { 252, 26, "Chittagong" },
                    { 253, 26, "Khulna" },
                    { 254, 26, "Rajshahi" },
                    { 255, 26, "Sylhet" },
                    { 256, 26, "Barisal" },
                    { 257, 26, "Rangpur" },
                    { 258, 26, "Comilla  Narayanganj" },
                    { 259, 26, "Mymensingh" },
                    { 260, 26, "Bogra" },
                    { 261, 27, "Ho Chi Minh City" },
                    { 262, 27, "Hanoi" },
                    { 263, 27, "Da Nang" },
                    { 264, 27, "Hai Phong" },
                    { 265, 27, "Can Tho" },
                    { 266, 27, "Hue" },
                    { 267, 27, "Nha Trang" },
                    { 268, 27, "Vung Tau" },
                    { 269, 27, "Bien Hoa" },
                    { 270, 27, "Thai Nguyen" },
                    { 271, 28, "Bangkok" },
                    { 272, 28, "Chiang Mai" },
                    { 273, 28, "Pattaya" },
                    { 274, 28, "Phuket" },
                    { 275, 28, "Hat Yai" },
                    { 276, 28, "Nakhon Ratchasima" },
                    { 277, 28, "Udon Thani" },
                    { 278, 28, "Surat Thani" },
                    { 279, 28, "Khon Kaen" },
                    { 280, 28, "Chiang Rai" },
                    { 281, 29, "Tehran" },
                    { 282, 29, "Mashhad" },
                    { 283, 29, "Isfahan" },
                    { 284, 29, "Shiraz" },
                    { 285, 29, "Tabriz" },
                    { 286, 29, "Karaj" },
                    { 287, 29, "Ahvaz" },
                    { 288, 29, "Qom" },
                    { 289, 29, "Kermanshah" },
                    { 290, 29, "Urmia" },
                    { 291, 30, "Tel Aviv" },
                    { 292, 30, "Jerusalem" },
                    { 293, 30, "Haifa" },
                    { 294, 30, "Rishon LeZion" },
                    { 295, 30, "Petah Tikva" },
                    { 296, 30, "Ashdod" },
                    { 297, 30, "Netanya" },
                    { 298, 30, "Beer Sheva" },
                    { 299, 30, "Holon" },
                    { 300, 30, "Bnei Brak" },
                    { 301, 31, "Kuala Lumpur" },
                    { 302, 31, "George Town" },
                    { 303, 31, "Johor Bahru" },
                    { 304, 31, "Ipoh" },
                    { 305, 31, "Shah Alam" },
                    { 306, 31, "Petaling Jaya" },
                    { 307, 31, "Kota Kinabalu" },
                    { 308, 31, "Malacca City" },
                    { 309, 31, "Alor Setar" },
                    { 310, 31, "Kuching" },
                    { 311, 32, "Quezon City" },
                    { 312, 32, "Manila" },
                    { 313, 32, "Davao City" },
                    { 314, 32, "Cebu City" },
                    { 315, 32, "Zamboanga City" },
                    { 316, 32, "Taguig" },
                    { 317, 32, "Pasig" },
                    { 318, 32, "Cagayan de Oro" },
                    { 319, 32, "Paranaque" },
                    { 320, 32, "Las Pinas" },
                    { 321, 33, "Lisbon" },
                    { 322, 33, "Porto" },
                    { 323, 33, "Amadora" },
                    { 324, 33, "Braga" },
                    { 325, 33, "Setubal" },
                    { 326, 33, "Coimbra" },
                    { 327, 33, "Queluz" },
                    { 328, 33, "Funchal" },
                    { 329, 33, "Vila Nova de Gaia" },
                    { 330, 33, "Almada" },
                    { 331, 34, "Athens" },
                    { 332, 34, "Thessaloniki" },
                    { 333, 34, "Patras" },
                    { 334, 34, "Heraklion" },
                    { 335, 34, "Larissa" },
                    { 336, 34, "Volos" },
                    { 337, 34, "Ioannina" },
                    { 338, 34, "Chania" },
                    { 339, 34, "Kavala" },
                    { 340, 34, "Kalamata" },
                    { 341, 35, "Amsterdam" },
                    { 342, 35, "Rotterdam" },
                    { 343, 35, "The Hague" },
                    { 344, 35, "Utrecht" },
                    { 345, 35, "Eindhoven" },
                    { 346, 35, "Tilburg" },
                    { 347, 35, "Groningen" },
                    { 348, 35, "Almere" },
                    { 349, 35, "Breda" },
                    { 350, 35, "Nijmegen" },
                    { 351, 36, "Stockholm" },
                    { 352, 36, "Gothenburg" },
                    { 353, 36, "Malmo" },
                    { 354, 36, "Uppsala" },
                    { 355, 36, "Vasteras" },
                    { 356, 36, "Orebro" },
                    { 357, 36, "Linkoping" },
                    { 358, 36, "Helsingborg" },
                    { 359, 36, "Jonkoping" },
                    { 360, 36, "Norrkoping" },
                    { 361, 37, "Oslo" },
                    { 362, 37, "Bergen" },
                    { 363, 37, "Trondheim" },
                    { 364, 37, "Stavanger" },
                    { 365, 37, "Drammen" },
                    { 366, 37, "Fredrikstad" },
                    { 367, 37, "Kristiansand" },
                    { 368, 37, "Sandnes" },
                    { 369, 37, "Tromsø" },
                    { 370, 37, "Ålesund" },
                    { 371, 38, "Copenhagen" },
                    { 372, 38, "Aarhus" },
                    { 373, 38, "Odense" },
                    { 374, 38, "Aalborg" },
                    { 375, 38, "Esbjerg" },
                    { 376, 38, "Randers" },
                    { 377, 38, "Kolding" },
                    { 378, 38, "Horsens" },
                    { 379, 38, "Vejle" },
                    { 380, 38, "Roskilde" },
                    { 381, 39, "Zurich" },
                    { 382, 39, "Geneva" },
                    { 383, 39, "Basel" },
                    { 384, 39, "Lausanne" },
                    { 385, 39, "Bern" },
                    { 386, 39, "Lucerne" },
                    { 387, 39, "St. Gallen" },
                    { 388, 39, "Lugano" },
                    { 389, 39, "Biel/Bienne" },
                    { 390, 39, "Thun" },
                    { 391, 40, "Warsaw" },
                    { 392, 40, "Krakow" },
                    { 393, 40, "Lodz" },
                    { 394, 40, "Wroclaw" },
                    { 395, 40, "Poznan" },
                    { 396, 40, "Gdansk" },
                    { 397, 40, "Szczecin" },
                    { 398, 40, "Bydgoszcz" },
                    { 399, 40, "Lublin" },
                    { 400, 40, "Katowice" },
                    { 401, 41, "Brussels" },
                    { 402, 41, "Antwerp" },
                    { 403, 41, "Ghent" },
                    { 404, 41, "Charleroi" },
                    { 405, 41, "Liege" },
                    { 406, 41, "Bruges" },
                    { 407, 41, "Namur" },
                    { 408, 41, "Leuven" },
                    { 409, 41, "Mons" },
                    { 410, 41, "Aalst" },
                    { 411, 42, "Bucharest" },
                    { 412, 42, "Cluj-Napoca" },
                    { 413, 42, "Timisoara" },
                    { 414, 42, "Iasi" },
                    { 415, 42, "Constanta" },
                    { 416, 42, "Craiova" },
                    { 417, 42, "Brasov" },
                    { 418, 42, "Galati" },
                    { 419, 42, "Ploiesti" },
                    { 420, 42, "Oradea" },
                    { 421, 43, "Prague" },
                    { 422, 43, "Brno" },
                    { 423, 43, "Ostrava" },
                    { 424, 43, "Plzen" },
                    { 425, 43, "Liberec" },
                    { 426, 43, "Olomouc" },
                    { 427, 43, "Ceske Budejovice" },
                    { 428, 43, "Hradec Kralove" },
                    { 429, 43, "Usti nad Labem" },
                    { 430, 43, "Pardubice" },
                    { 431, 44, "Budapest" },
                    { 432, 44, "Debrecen" },
                    { 433, 44, "Szeged" },
                    { 434, 44, "Miskolc" },
                    { 435, 44, "Pecs" },
                    { 436, 44, "Gyor" },
                    { 437, 44, "Nyiregyhaza" },
                    { 438, 44, "Kecskemet" },
                    { 439, 44, "Szekesfehervar" },
                    { 440, 44, "Szombathely" },
                    { 441, 45, "Kyiv" },
                    { 442, 45, "Kharkiv" },
                    { 443, 45, "Odesa" },
                    { 444, 45, "Dnipro" },
                    { 445, 45, "Donetsk" },
                    { 446, 45, "Zaporizhzhia" },
                    { 447, 45, "Lviv" },
                    { 448, 45, "Kryvyi Rih" },
                    { 449, 45, "Mykolaiv" },
                    { 450, 45, "Mariupol" },
                    { 451, 46, "Bogota" },
                    { 452, 46, "Medellin" },
                    { 453, 46, "Cali" },
                    { 454, 46, "Barranquilla" },
                    { 455, 46, "Cartagena" },
                    { 456, 46, "Bucaramanga" },
                    { 457, 46, "Cucuta" },
                    { 458, 46, "Pereira" },
                    { 459, 46, "Manizales" },
                    { 460, 46, "Ibague" },
                    { 461, 47, "Santiago" },
                    { 462, 47, "Valparaiso" },
                    { 463, 47, "Concepcion" },
                    { 464, 47, "Antofagasta" },
                    { 465, 47, "Vina del Mar" },
                    { 466, 47, "Temuco" },
                    { 467, 47, "Rancagua" },
                    { 468, 47, "Talca" },
                    { 469, 47, "Arica" },
                    { 470, 47, "Puerto Montt" },
                    { 471, 48, "Lima" },
                    { 472, 48, "Arequipa" },
                    { 473, 48, "Trujillo" },
                    { 474, 48, "Chiclayo" },
                    { 475, 48, "Piura" },
                    { 476, 48, "Cusco" },
                    { 477, 48, "Iquitos" },
                    { 478, 48, "Huancayo" },
                    { 479, 48, "Chimbote" },
                    { 480, 48, "Pucallpa" },
                    { 481, 49, "Caracas" },
                    { 482, 49, "Maracaibo" },
                    { 483, 49, "Valencia" },
                    { 484, 49, "Barquisimeto" },
                    { 485, 49, "Maracay" },
                    { 486, 49, "Ciudad Guayana" },
                    { 487, 49, "Barcelona" },
                    { 488, 49, "Maturin" },
                    { 489, 49, "Cumana" },
                    { 490, 49, "Puerto La Cruz" },
                    { 491, 50, "Quito" },
                    { 492, 50, "Guayaquil" },
                    { 493, 50, "Cuenca" },
                    { 494, 50, "Santo Domingo" },
                    { 495, 50, "Machala" },
                    { 496, 50, "Ambato" },
                    { 497, 50, "Portoviejo" },
                    { 498, 50, "Loja" },
                    { 499, 50, "Esmeraldas" },
                    { 500, 50, "Ibarra" },
                    { 501, 51, "Dubai" },
                    { 502, 51, "Abu Dhabi" },
                    { 503, 51, "Sharjah" },
                    { 504, 51, "Al Ain" },
                    { 505, 51, "Ajman" },
                    { 506, 51, "Ras Al Khaimah" },
                    { 507, 51, "Fujairah" },
                    { 508, 51, "Umm Al Quwain" },
                    { 509, 51, "Khor Fakkan" },
                    { 510, 51, "Dibba Al-Fujairah" },
                    { 511, 52, "Singapore" },
                    { 512, 52, "Woodlands" },
                    { 513, 52, "Jurong West" },
                    { 514, 52, "Tampines" },
                    { 515, 52, "Sengkang" },
                    { 516, 52, "Bedok" },
                    { 517, 52, "Yishun" },
                    { 518, 52, "Hougang" },
                    { 519, 52, "Ang Mo Kio" },
                    { 520, 52, "Punggol" },
                    { 521, 53, "Doha" },
                    { 522, 53, "Al Rayyan" },
                    { 523, 53, "Al Wakrah" },
                    { 524, 53, "Al Khor" },
                    { 525, 53, "Umm Salal" },
                    { 526, 53, "Dukhan" },
                    { 527, 53, "Mesaieed" },
                    { 528, 53, "Madinat ash Shamal" },
                    { 529, 53, "Al Shamal" },
                    { 530, 53, "Al Ghuwariyah" },
                    { 531, 54, "Juba" },
                    { 532, 54, "Wau" },
                    { 533, 54, "Malakal" },
                    { 534, 54, "Yei" },
                    { 535, 54, "Bor" },
                    { 536, 54, "Rumbek" },
                    { 537, 54, "Torit" },
                    { 538, 54, "Yambio" },
                    { 539, 54, "Aweil" },
                    { 540, 54, "Nimule" },
                    { 541, 55, "Yangon" },
                    { 542, 55, "Mandalay" },
                    { 543, 55, "Naypyidaw" },
                    { 544, 55, "Mawlamyine" },
                    { 545, 55, "Bago" },
                    { 546, 55, "Pathein" },
                    { 547, 55, "Monywa" },
                    { 548, 55, "Sittwe" },
                    { 549, 55, "Myitkyina" },
                    { 550, 55, "Taunggyi" },
                    { 551, 56, "Astana" },
                    { 552, 56, "Almaty" },
                    { 553, 56, "Shymkent" },
                    { 554, 56, "Karaganda" },
                    { 555, 56, "Aktobe" },
                    { 556, 56, "Taraz" },
                    { 557, 56, "Pavlodar" },
                    { 558, 56, "Ust-Kamenogorsk" },
                    { 559, 56, "Semey" },
                    { 560, 56, "Kostanay" },
                    { 561, 57, "Algiers" },
                    { 562, 57, "Oran" },
                    { 563, 57, "Constantine" },
                    { 564, 57, "Annaba" },
                    { 565, 57, "Blida" },
                    { 566, 57, "Batna" },
                    { 567, 57, "Djelfa" },
                    { 568, 57, "Sétif" },
                    { 569, 57, "Sidi Bel Abbes" },
                    { 570, 57, "Biskra" },
                    { 571, 58, "Casablanca" },
                    { 572, 58, "Rabat" },
                    { 573, 58, "Fes" },
                    { 574, 58, "Marrakesh" },
                    { 575, 58, "Tangier" },
                    { 576, 58, "Agadir" },
                    { 577, 58, "Meknes" },
                    { 578, 58, "Oujda" },
                    { 579, 58, "Kenitra" },
                    { 580, 58, "Tetouan" },
                    { 581, 59, "Tunis" },
                    { 582, 59, "Sfax" },
                    { 583, 59, "Sousse" },
                    { 584, 59, "Kairouan" },
                    { 585, 59, "Bizerte" },
                    { 586, 59, "Gabes" },
                    { 587, 59, "Ariana" },
                    { 588, 59, "Gafsa" },
                    { 589, 59, "Monastir" },
                    { 590, 59, "Ben Arous" },
                    { 591, 60, "Beirut" },
                    { 592, 60, "Tripoli" },
                    { 593, 60, "Sidon" },
                    { 594, 60, "Tyre" },
                    { 595, 60, "Jounieh" },
                    { 596, 60, "Zahle" },
                    { 597, 60, "Baalbek" },
                    { 598, 60, "Byblos" },
                    { 599, 60, "Nabatieh" },
                    { 600, 60, "Batroun" },
                    { 601, 61, "Amman" },
                    { 602, 61, "Zarqa" },
                    { 603, 61, "Irbid" },
                    { 604, 61, "Aqaba" },
                    { 605, 61, "Madaba" },
                    { 606, 61, "Salt" },
                    { 607, 61, "Jerash" },
                    { 608, 61, "Karak" },
                    { 609, 61, "Mafraq" },
                    { 610, 61, "Tafilah" },
                    { 611, 62, "Colombo" },
                    { 612, 62, "Kandy" },
                    { 613, 62, "Galle" },
                    { 614, 62, "Jaffna" },
                    { 615, 62, "Negombo" },
                    { 616, 62, "Ratnapura" },
                    { 617, 62, "Batticaloa" },
                    { 618, 62, "Trincomalee" },
                    { 619, 62, "Anuradhapura" },
                    { 620, 62, "Matara" },
                    { 621, 63, "Kathmandu" },
                    { 622, 63, "Pokhara" },
                    { 623, 63, "Lalitpur" },
                    { 624, 63, "Biratnagar" },
                    { 625, 63, "Bharatpur" },
                    { 626, 63, "Birgunj" },
                    { 627, 63, "Hetauda" },
                    { 628, 63, "Nepalgunj" },
                    { 629, 63, "Dhangadhi" },
                    { 630, 63, "Janakpur" },
                    { 631, 64, "Ulaanbaatar" },
                    { 632, 64, "Erdenet" },
                    { 633, 64, "Darkhan" },
                    { 634, 64, "Choibalsan" },
                    { 635, 64, "Mörön" },
                    { 636, 64, "Khovd" },
                    { 637, 64, "Ölgii" },
                    { 638, 64, "Ulaangom" },
                    { 639, 64, "Sainshand" },
                    { 640, 64, "Tsetserleg" },
                    { 641, 65, "Tashkent" },
                    { 642, 65, "Samarkand" },
                    { 643, 65, "Namangan" },
                    { 644, 65, "Andijan" },
                    { 645, 65, "Bukhara" },
                    { 646, 65, "Fergana" },
                    { 647, 65, "Jizzakh" },
                    { 648, 65, "Urgench" },
                    { 649, 65, "Nukus" },
                    { 650, 65, "Qarshi" },
                    { 651, 66, "Ashgabat" },
                    { 652, 66, "Türkmenabat" },
                    { 653, 66, "Daşoguz" },
                    { 654, 66, "Mary" },
                    { 655, 66, "Balkanabat" },
                    { 656, 66, "Bayramaly" },
                    { 657, 66, "Türkmenbaşy" },
                    { 658, 66, "Tejen" },
                    { 659, 66, "Kerki" },
                    { 660, 66, "Seydi" },
                    { 661, 67, "La Paz" },
                    { 662, 67, "Santa Cruz de la Sierra" },
                    { 663, 67, "Cochabamba" },
                    { 664, 67, "Sucre" },
                    { 665, 67, "Oruro" },
                    { 666, 67, "Potosí" },
                    { 667, 67, "Tarija" },
                    { 668, 67, "Trinidad" },
                    { 669, 67, "Cobija" },
                    { 670, 67, "Riberalta" },
                    { 671, 68, "Asunción" },
                    { 672, 68, "Ciudad del Este" },
                    { 673, 68, "Encarnación" },
                    { 674, 68, "Luque" },
                    { 675, 68, "San Lorenzo" },
                    { 676, 68, "Capiatá" },
                    { 677, 68, "Lambaré" },
                    { 678, 68, "Fernando de la Mora" },
                    { 679, 68, "Pedro Juan Caballero" },
                    { 680, 68, "Coronel Oviedo" },
                    { 681, 69, "Montevideo" },
                    { 682, 69, "Salto" },
                    { 683, 69, "Paysandú" },
                    { 684, 69, "Maldonado" },
                    { 685, 69, "Rivera" },
                    { 686, 69, "Las Piedras" },
                    { 687, 69, "Tacuarembó" },
                    { 688, 69, "Mercedes" },
                    { 689, 69, "Artigas" },
                    { 690, 69, "Minas" },
                    { 691, 70, "San José" },
                    { 692, 70, "Alajuela" },
                    { 693, 70, "Cartago" },
                    { 694, 70, "Heredia" },
                    { 695, 70, "Puntarenas" },
                    { 696, 70, "Limón" },
                    { 697, 70, "Liberia" },
                    { 698, 70, "San Isidro de El General" },
                    { 699, 70, "Quesada" },
                    { 700, 70, "Desamparados" },
                    { 701, 71, "Panama City" },
                    { 702, 71, "Colón" },
                    { 703, 71, "David" },
                    { 704, 71, "La Chorrera" },
                    { 705, 71, "Santiago" },
                    { 706, 71, "Chitré" },
                    { 707, 71, "Penonomé" },
                    { 708, 71, "Las Tablas" },
                    { 709, 71, "Bocas del Toro" },
                    { 710, 71, "Aguadulce" },
                    { 711, 72, "Tegucigalpa" },
                    { 712, 72, "San Pedro Sula" },
                    { 713, 72, "Choloma" },
                    { 714, 72, "La Ceiba" },
                    { 715, 72, "El Progreso" },
                    { 716, 72, "Comayagua" },
                    { 717, 72, "Puerto Cortés" },
                    { 718, 72, "Juticalpa" },
                    { 719, 72, "Siguatepeque" },
                    { 720, 72, "Santa Rosa de Copán" },
                    { 721, 73, "San Salvador" },
                    { 722, 73, "Santa Ana" },
                    { 723, 73, "San Miguel" },
                    { 724, 73, "Soyapango" },
                    { 725, 73, "Mejicanos" },
                    { 726, 73, "Apopa" },
                    { 727, 73, "Santa Tecla" },
                    { 728, 73, "Delgado" },
                    { 729, 73, "Sonsonate" },
                    { 730, 73, "Usulután" },
                    { 731, 74, "Guatemala City" },
                    { 732, 74, "Quetzaltenango" },
                    { 733, 74, "Mixco" },
                    { 734, 74, "Villa Nueva" },
                    { 735, 74, "Escuintla" },
                    { 736, 74, "San Juan Sacatepéquez" },
                    { 737, 74, "Chimaltenango" },
                    { 738, 74, "Huehuetenango" },
                    { 739, 74, "Coban" },
                    { 740, 74, "Chichicastenango" },
                    { 741, 75, "Santo Domingo" },
                    { 742, 75, "Santiago de los Caballeros" },
                    { 743, 75, "La Romana" },
                    { 744, 75, "San Pedro de Macorís" },
                    { 745, 75, "San Francisco de Macorís" },
                    { 746, 75, "Puerto Plata" },
                    { 747, 75, "La Vega" },
                    { 748, 75, "Baní" },
                    { 749, 75, "San Cristóbal" },
                    { 750, 75, "Barahona" },
                    { 751, 76, "Kuwait City" },
                    { 752, 76, "Hawalli" },
                    { 753, 76, "Al Farwaniyah" },
                    { 754, 76, "Mubarak Al-Kabeer" },
                    { 755, 76, "Al Ahmadi" },
                    { 756, 76, "Jahra" },
                    { 757, 76, "Fintas" },
                    { 758, 76, "Salmiya" },
                    { 759, 76, "Sabah Al-Salem" },
                    { 760, 76, "Mangaf" },
                    { 761, 77, "Manama" },
                    { 762, 77, "Riffa" },
                    { 763, 77, "Muharraq" },
                    { 764, 77, "Hamad Town" },
                    { 765, 77, "A'ali" },
                    { 766, 77, "Isa Town" },
                    { 767, 77, "Sitra" },
                    { 768, 77, "Budaiya" },
                    { 769, 77, "Jidhafs" },
                    { 770, 77, "Seef" },
                    { 771, 78, "Muscat" },
                    { 772, 78, "Salalah" },
                    { 773, 78, "Sohar" },
                    { 774, 78, "Nizwa" },
                    { 775, 78, "Sur" },
                    { 776, 78, "Ibri" },
                    { 777, 78, "Rustaq" },
                    { 778, 78, "Barka" },
                    { 779, 78, "Khasab" },
                    { 780, 78, "Al Buraimi" },
                    { 781, 79, "Baghdad" },
                    { 782, 79, "Basra" },
                    { 783, 79, "Mosul" },
                    { 784, 79, "Erbil" },
                    { 785, 79, "Najaf" },
                    { 786, 79, "Karbala" },
                    { 787, 79, "Sulaymaniyah" },
                    { 788, 79, "Kirkuk" },
                    { 789, 79, "Hilla" },
                    { 790, 79, "Diwaniyah" },
                    { 791, 80, "Damascus" },
                    { 792, 80, "Aleppo" },
                    { 793, 80, "Homs" },
                    { 794, 80, "Latakia" },
                    { 795, 80, "Hama" },
                    { 796, 80, "Deir ez-Zor" },
                    { 797, 80, "Raqqa" },
                    { 798, 80, "Tartus" },
                    { 799, 80, "Daraa" },
                    { 800, 80, "Idlib" },
                    { 801, 81, "Sana'a" },
                    { 802, 81, "Aden" },
                    { 803, 81, "Taiz" },
                    { 804, 81, "Hodeidah" },
                    { 805, 81, "Ibb" },
                    { 806, 81, "Dhamar" },
                    { 807, 81, "Mukalla" },
                    { 808, 81, "Zabid" },
                    { 809, 81, "Sa'dah" },
                    { 810, 81, "Al Bayda" },
                    { 811, 82, "Khartoum" },
                    { 812, 82, "Omdurman" },
                    { 813, 82, "Port Sudan" },
                    { 814, 82, "Kassala" },
                    { 815, 82, "El Obeid" },
                    { 816, 82, "Nyala" },
                    { 817, 82, "Wad Medani" },
                    { 818, 82, "El Fasher" },
                    { 819, 82, "Atbara" },
                    { 820, 82, "Gedaref" },
                    { 821, 83, "Addis Ababa" },
                    { 822, 83, "Dire Dawa" },
                    { 823, 83, "Mekelle" },
                    { 824, 83, "Gondar" },
                    { 825, 83, "Adama" },
                    { 826, 83, "Hawassa" },
                    { 827, 83, "Bahir Dar" },
                    { 828, 83, "Jimma" },
                    { 829, 83, "Dessie" },
                    { 830, 83, "Jijiga" },
                    { 831, 84, "Dodoma" },
                    { 832, 84, "Dar es Salaam" },
                    { 833, 84, "Mwanza" },
                    { 834, 84, "Arusha" },
                    { 835, 84, "Mbeya" },
                    { 836, 84, "Zanzibar City" },
                    { 837, 84, "Tanga" },
                    { 838, 84, "Morogoro" },
                    { 839, 84, "Kigoma" },
                    { 840, 84, "Songea" },
                    { 841, 85, "Lusaka" },
                    { 842, 85, "Ndola" },
                    { 843, 85, "Kitwe" },
                    { 844, 85, "Kabwe" },
                    { 845, 85, "Chingola" },
                    { 846, 85, "Mufulira" },
                    { 847, 85, "Livingstone" },
                    { 848, 85, "Luanshya" },
                    { 849, 85, "Chipata" },
                    { 850, 85, "Kasama" },
                    { 851, 86, "Harare" },
                    { 852, 86, "Bulawayo" },
                    { 853, 86, "Chitungwiza" },
                    { 854, 86, "Mutare" },
                    { 855, 86, "Gweru" },
                    { 856, 86, "Kwekwe" },
                    { 857, 86, "Kadoma" },
                    { 858, 86, "Masvingo" },
                    { 859, 86, "Chinhoyi" },
                    { 860, 86, "Bindura" },
                    { 861, 87, "Gaborone" },
                    { 862, 87, "Francistown" },
                    { 863, 87, "Molepolole" },
                    { 864, 87, "Serowe" },
                    { 865, 87, "Selibe Phikwe" },
                    { 866, 87, "Maun" },
                    { 867, 87, "Kanye" },
                    { 868, 87, "Mochudi" },
                    { 869, 87, "Lobatse" },
                    { 870, 87, "Palapye" },
                    { 871, 88, "Windhoek" },
                    { 872, 88, "Walvis Bay" },
                    { 873, 88, "Swakopmund" },
                    { 874, 88, "Rundu" },
                    { 875, 88, "Oshakati" },
                    { 876, 88, "Katima Mulilo" },
                    { 877, 88, "Otjiwarongo" },
                    { 878, 88, "Keetmanshoop" },
                    { 879, 88, "Tsumeb" },
                    { 880, 88, "Gobabis" },
                    { 881, 89, "Antananarivo" },
                    { 882, 89, "Toamasina" },
                    { 883, 89, "Antsirabe" },
                    { 884, 89, "Fianarantsoa" },
                    { 885, 89, "Mahajanga" },
                    { 886, 89, "Toliara" },
                    { 887, 89, "Morondava" },
                    { 888, 89, "Ambatondrazaka" },
                    { 889, 89, "Manakara" },
                    { 890, 89, "Nosy Be" },
                    { 891, 90, "Port Moresby" },
                    { 892, 90, "Lae" },
                    { 893, 90, "Madang" },
                    { 894, 90, "Mount Hagen" },
                    { 895, 90, "Goroka" },
                    { 896, 90, "Rabaul" },
                    { 897, 90, "Kimbe" },
                    { 898, 90, "Wewak" },
                    { 899, 90, "Kavieng" },
                    { 900, 90, "Alotau" },
                    { 901, 91, "Suva" },
                    { 902, 91, "Nadi" },
                    { 903, 91, "Lautoka" },
                    { 904, 91, "Labasa" },
                    { 905, 91, "Ba" },
                    { 906, 91, "Levuka" },
                    { 907, 91, "Savusavu" },
                    { 908, 91, "Rakiraki" },
                    { 909, 91, "Tavua" },
                    { 910, 91, "Sigatoka" },
                    { 911, 92, "Honiara" },
                    { 912, 92, "Gizo" },
                    { 913, 92, "Auki" },
                    { 914, 92, "Tulagi" },
                    { 915, 92, "Kirakira" },
                    { 916, 92, "Lata" },
                    { 917, 92, "Munda" },
                    { 918, 92, "Taro" },
                    { 919, 92, "Buala" },
                    { 920, 92, "Noro" },
                    { 921, 93, "Bandar Seri Begawan" },
                    { 922, 93, "Kuala Belait" },
                    { 923, 93, "Seria" },
                    { 924, 93, "Tutong" },
                    { 925, 93, "Bangar" },
                    { 926, 93, "Muara" },
                    { 927, 93, "Temburong" },
                    { 928, 93, "Lumut" },
                    { 929, 93, "Jerudong" },
                    { 930, 93, "Lamunin" },
                    { 931, 94, "Lilongwe" },
                    { 932, 94, "Blantyre" },
                    { 933, 94, "Mzuzu" },
                    { 934, 94, "Zomba" },
                    { 935, 94, "Kasungu" },
                    { 936, 94, "Mangochi" },
                    { 937, 94, "Karonga" },
                    { 938, 94, "Salima" },
                    { 939, 94, "Nkhotakota" },
                    { 940, 94, "Mchinji" },
                    { 941, 95, "Gitega" },
                    { 942, 95, "Ngozi" },
                    { 943, 95, "Ruyigi" },
                    { 944, 95, "Kayanza" },
                    { 945, 95, "Muyinga" },
                    { 946, 95, "Makamba" },
                    { 947, 95, "Bururi" },
                    { 948, 95, "Cibitoke" },
                    { 949, 95, "Bubanza" },
                    { 950, 95, "Karuzi" },
                    { 951, 96, "Kigali" },
                    { 952, 96, "Gisenyi" },
                    { 953, 96, "Ruhengeri" },
                    { 954, 96, "Butare" },
                    { 955, 96, "Muhanga" },
                    { 956, 96, "Byumba" },
                    { 957, 96, "Cyangugu" },
                    { 958, 96, "Nyanza" },
                    { 959, 96, "Kibuye" },
                    { 960, 96, "Rwamagana" },
                    { 961, 97, "Abidjan" },
                    { 962, 97, "Yamoussoukro" },
                    { 963, 97, "Bouaké" },
                    { 964, 97, "Daloa" },
                    { 965, 97, "Korhogo" },
                    { 966, 97, "San-Pédro" },
                    { 967, 97, "Man" },
                    { 968, 97, "Gagnoa" },
                    { 969, 97, "Abengourou" },
                    { 970, 97, "Divo" },
                    { 971, 98, "Dakar" },
                    { 972, 98, "Thiès" },
                    { 973, 98, "Saint-Louis" },
                    { 974, 98, "Kaolack" },
                    { 975, 98, "Ziguinchor" },
                    { 976, 98, "Rufisque" },
                    { 977, 98, "Mbour" },
                    { 978, 98, "Diourbel" },
                    { 979, 98, "Louga" },
                    { 980, 98, "Tambacounda" },
                    { 981, 99, "Accra" },
                    { 982, 99, "Kumasi" },
                    { 983, 99, "Tamale" },
                    { 984, 99, "Sekondi-Takoradi" },
                    { 985, 99, "Tema" },
                    { 986, 99, "Cape Coast" },
                    { 987, 99, "Koforidua" },
                    { 988, 99, "Wa" },
                    { 989, 99, "Ho" },
                    { 990, 99, "Sunyani" },
                    { 991, 100, "Yaoundé" },
                    { 992, 100, "Douala" },
                    { 993, 100, "Bamenda" },
                    { 994, 100, "Garoua" },
                    { 995, 100, "Bafoussam" },
                    { 996, 100, "Maroua" },
                    { 997, 100, "Ngaoundéré" },
                    { 998, 100, "Kumba" },
                    { 999, 100, "Buea" },
                    { 1000, 100, "Limbe" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 247);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 248);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 249);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 250);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 251);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 252);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 253);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 254);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 255);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 256);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 257);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 258);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 259);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 260);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 261);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 262);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 263);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 264);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 265);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 266);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 267);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 268);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 269);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 270);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 271);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 272);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 273);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 274);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 275);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 276);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 277);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 278);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 279);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 280);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 281);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 282);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 283);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 284);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 285);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 286);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 287);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 288);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 289);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 290);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 291);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 292);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 293);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 294);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 295);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 296);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 297);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 298);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 299);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 301);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 302);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 303);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 304);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 305);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 306);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 307);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 308);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 309);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 310);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 311);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 312);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 313);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 314);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 315);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 316);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 317);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 318);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 319);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 320);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 321);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 322);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 323);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 324);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 325);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 326);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 327);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 328);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 329);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 330);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 331);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 332);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 334);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 335);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 336);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 337);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 338);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 339);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 340);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 341);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 342);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 343);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 344);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 345);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 346);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 347);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 348);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 349);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 350);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 351);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 352);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 353);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 354);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 355);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 356);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 357);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 358);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 359);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 360);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 361);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 362);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 363);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 364);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 365);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 366);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 367);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 368);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 369);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 370);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 371);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 372);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 373);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 374);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 375);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 376);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 377);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 378);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 379);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 380);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 381);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 382);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 383);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 384);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 385);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 386);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 387);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 388);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 389);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 390);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 391);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 392);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 393);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 394);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 395);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 396);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 397);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 398);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 399);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 400);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 401);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 402);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 403);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 404);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 405);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 406);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 407);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 408);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 409);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 410);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 411);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 412);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 413);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 414);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 415);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 416);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 417);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 418);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 419);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 420);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 421);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 422);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 423);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 424);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 425);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 426);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 427);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 428);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 429);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 430);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 431);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 432);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 433);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 434);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 435);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 436);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 437);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 438);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 439);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 440);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 441);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 442);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 443);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 444);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 445);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 446);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 447);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 448);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 449);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 450);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 451);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 452);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 453);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 454);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 455);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 456);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 457);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 458);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 459);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 460);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 461);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 462);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 463);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 464);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 465);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 466);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 467);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 468);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 469);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 470);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 471);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 472);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 473);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 474);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 475);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 476);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 477);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 478);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 479);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 480);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 481);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 482);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 483);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 484);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 485);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 486);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 487);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 488);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 489);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 490);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 491);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 492);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 493);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 494);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 495);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 496);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 497);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 498);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 499);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 500);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 501);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 502);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 503);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 504);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 505);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 506);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 507);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 508);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 509);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 510);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 511);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 512);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 513);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 514);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 515);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 516);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 517);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 518);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 519);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 520);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 521);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 522);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 523);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 524);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 525);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 526);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 527);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 528);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 529);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 530);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 531);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 532);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 533);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 534);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 535);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 536);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 537);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 538);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 539);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 540);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 541);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 542);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 543);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 544);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 545);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 546);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 547);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 548);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 549);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 550);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 551);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 552);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 553);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 554);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 555);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 556);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 557);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 558);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 559);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 560);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 561);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 562);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 563);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 564);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 565);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 566);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 567);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 568);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 569);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 570);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 571);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 572);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 573);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 574);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 575);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 576);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 577);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 578);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 579);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 580);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 581);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 582);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 583);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 584);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 585);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 586);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 587);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 588);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 589);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 590);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 591);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 592);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 593);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 594);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 595);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 596);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 597);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 598);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 599);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 600);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 601);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 602);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 603);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 604);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 605);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 606);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 607);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 608);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 609);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 610);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 611);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 612);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 613);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 614);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 615);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 616);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 617);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 618);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 619);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 620);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 621);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 622);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 623);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 624);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 625);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 626);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 627);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 628);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 629);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 630);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 631);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 632);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 633);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 634);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 635);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 636);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 637);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 638);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 639);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 640);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 641);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 642);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 643);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 644);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 645);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 646);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 647);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 648);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 649);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 650);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 651);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 652);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 653);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 654);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 655);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 656);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 657);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 658);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 659);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 660);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 661);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 662);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 663);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 664);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 665);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 666);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 667);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 668);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 669);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 670);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 671);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 672);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 673);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 674);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 675);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 676);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 677);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 678);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 679);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 680);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 681);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 682);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 683);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 684);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 685);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 686);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 687);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 688);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 689);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 690);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 691);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 692);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 693);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 694);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 695);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 696);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 697);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 698);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 699);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 700);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 701);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 702);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 703);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 704);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 705);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 706);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 707);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 708);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 709);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 710);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 711);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 712);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 713);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 714);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 715);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 716);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 717);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 718);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 719);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 720);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 721);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 722);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 723);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 724);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 725);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 726);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 727);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 728);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 729);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 730);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 731);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 732);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 733);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 734);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 735);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 736);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 737);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 738);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 739);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 740);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 741);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 742);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 743);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 744);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 745);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 746);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 747);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 748);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 749);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 750);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 751);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 752);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 753);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 754);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 755);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 756);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 757);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 758);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 759);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 760);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 761);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 762);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 763);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 764);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 765);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 766);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 767);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 768);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 769);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 770);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 771);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 772);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 773);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 774);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 775);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 776);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 777);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 778);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 779);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 780);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 781);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 782);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 783);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 784);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 785);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 786);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 787);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 788);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 789);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 790);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 791);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 792);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 793);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 794);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 795);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 796);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 797);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 798);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 799);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 800);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 801);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 802);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 803);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 804);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 805);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 806);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 807);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 808);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 809);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 810);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 811);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 812);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 813);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 814);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 815);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 816);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 817);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 818);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 819);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 820);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 821);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 822);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 823);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 824);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 825);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 826);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 827);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 828);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 829);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 830);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 831);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 832);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 833);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 834);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 835);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 836);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 837);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 838);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 839);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 840);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 841);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 842);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 843);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 844);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 845);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 846);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 847);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 848);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 849);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 850);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 851);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 852);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 853);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 854);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 855);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 856);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 857);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 858);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 859);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 860);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 861);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 862);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 863);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 864);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 865);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 866);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 867);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 868);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 869);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 870);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 871);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 872);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 873);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 874);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 875);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 876);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 877);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 878);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 879);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 880);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 881);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 882);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 883);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 884);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 885);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 886);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 887);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 888);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 889);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 890);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 891);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 892);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 893);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 894);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 895);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 896);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 897);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 898);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 899);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 900);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 901);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 902);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 903);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 904);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 905);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 906);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 907);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 908);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 909);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 910);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 911);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 912);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 913);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 914);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 915);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 916);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 917);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 918);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 919);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 920);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 921);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 922);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 923);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 924);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 925);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 926);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 927);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 928);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 929);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 930);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 931);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 932);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 933);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 934);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 935);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 936);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 937);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 938);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 939);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 940);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 941);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 942);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 943);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 944);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 945);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 946);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 947);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 948);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 949);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 950);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 951);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 952);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 953);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 954);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 955);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 956);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 957);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 958);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 959);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 960);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 961);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 962);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 963);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 964);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 965);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 966);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 967);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 968);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 969);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 970);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 971);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 972);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 973);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 974);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 975);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 976);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 977);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 978);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 979);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 980);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 981);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 982);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 983);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 984);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 985);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 986);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 987);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 988);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 989);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 990);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 991);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 992);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 993);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 994);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 995);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 996);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 997);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 998);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 999);

            migrationBuilder.DeleteData(
                schema: "Geography",
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1000);
        }
    }
}
