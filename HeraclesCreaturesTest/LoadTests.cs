using HeraclesCreatures;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace HeraclesCreaturesTest
{
    [TestClass]
    public class LoadTests
    {
        [TestMethod]
        public void GetPlayerData_Returns_PlayerData_With_Empty_Creatures_And_Items()
        {
            // Arrange
            Player player = new Player("TestPlayer", new List<Creatures>());

            // Act
            PlayerData playerData = player.GetPlayerData();

            // Assert
            Assert.IsNotNull(playerData);
            Assert.AreEqual("TestPlayer", playerData._name);
            Assert.AreEqual(-1, playerData._currentCreatureID);
            Assert.AreEqual(0, playerData._creatures.Count);
            Assert.AreEqual(0, playerData._items.Count);
        }

        [TestMethod]
        public void GetPlayerData_Returns_PlayerData_With_Single_Creature()
        {
            // Arrange
            List<Creatures> creatures = new List<Creatures>();
            CreatureStats stats = new CreatureStats();
            List<Moves> moves = new List<Moves>();
            moves.Add(new Moves("test", new MoveStats()));
            creatures.Add(new Creatures("Nemean Lion", stats, moves));
            Player player = new Player("TestPlayer", creatures);

            // Act
            PlayerData playerData = player.GetPlayerData();

            // Assert
            Assert.IsNotNull(playerData);
            Assert.AreEqual("TestPlayer", playerData._name);
            Assert.AreEqual(0, playerData._currentCreatureID);
            Assert.AreEqual(1, playerData._creatures.Count);
            Assert.AreEqual(0, playerData._items.Count);
        }

        [TestMethod]
        public void GetPlayerData_Returns_PlayerData_With_No_Items()
        {
            // Arrange
            List<Creatures> creatures = new List<Creatures>();
            CreatureStats stats = new CreatureStats();
            List<Moves> moves = new List<Moves>();
            moves.Add(new Moves("test", new MoveStats()));
            creatures.Add(new Creatures("Nemean Lion", stats, moves));
            Player player = new Player("TestPlayer", creatures);
            player.Items = new List<Items>(); // No items

            // Act
            PlayerData playerData = player.GetPlayerData();

            // Assert
            Assert.IsNotNull(playerData);
            Assert.AreEqual("TestPlayer", playerData._name);
            Assert.AreEqual(0, playerData._currentCreatureID);
            Assert.AreEqual(1, playerData._creatures.Count);
            Assert.AreEqual(0, playerData._items.Count);
        }

        [TestMethod]
        public void GetCreatureData_Returns_CreatureData_With_Correct_Values()
        {
            // Arrange
            CreatureStats creatureStats = new CreatureStats();
            List<Moves> moves = new List<Moves>();
            moves.Add(new Moves("test", new MoveStats()));
            moves.Add(new Moves("test2", new MoveStats()));
            moves.Add(new Moves("test3", new MoveStats()));
            Creatures creature = new Creatures("Lion", creatureStats, moves);

            // Act
            CreatureData creatureData = creature.GetCreatureData();

            // Assert
            Assert.IsNotNull(creatureData);
            Assert.AreEqual("Lion", creatureData._creatureName);
            Assert.AreEqual(3, creatureData._moveData.Count);
        }

        [TestMethod]
        public void GetMoveData_Returns_MoveData_With_Correct_Values()
        {
            // Arrange
            Moves move = new Moves("test", new MoveStats());

            // Act
            MoveData moveData = move.GetMoveData();

            // Assert
            Assert.IsNotNull(moveData);
            Assert.AreEqual("test", moveData._moveName);
            Assert.AreEqual(10, moveData._stats.Power);
            Assert.AreEqual(10, moveData._stats.Accuracy);
            Assert.AreEqual(5, moveData._stats.CritRate);
            Assert.AreEqual(6, moveData._stats.MaxPP);
            Assert.AreEqual(10, moveData._stats.ManaCost);
            Assert.AreEqual("Fire", moveData._stats.Type);
        }

        [TestMethod]
        public void GetItemsData_Returns_ItemData_With_Correct_Values()
        {
            // Arrange
            Items item = new Potion();

            // Act
            ItemData itemData = item.GetItemsData();

            // Assert
            Assert.IsNotNull(itemData);
            Assert.AreEqual("Potion", itemData._itemName);
        }
    }
}
