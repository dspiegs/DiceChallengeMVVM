using System.Collections.Generic;
using DiceChallengeMVVM.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DiceChallengeMVVM.Test
{
    [TestClass]
    public class RuleTests
    {
        private List<Dice> orderedDice;
        private Rule straightRule;

        [TestInitialize]
        public void Setup()
        {
            orderedDice = new List<Dice>
            {
                new Dice
                {
                    Value = 1
                },
                new Dice
                {
                    Value = 2
                },
                new Dice
                {
                    Value = 3
                },
                new Dice
                {
                    Value = 4
                },
                new Dice
                {
                    Value = 5
                },
                new Dice
                {
                    Value = 6
                }
            };

            //i would never do this in productionCode
            var rules = Game.GetRules();
            straightRule = rules[3];
        }

        #region Rule Tests

        [TestMethod]
        public void Test_No_Dice()
        {
            var empty = new List<Dice>();
            var rule = new CustomRule();
            Assert.IsFalse(rule.PassesRule(empty));
        }

        [TestMethod]
        public void Test_Null()
        {
            var rule = new KindRule(4);
            Assert.IsFalse(rule.PassesRule(null));
        }

        #endregion

        #region Straight Tests

        [TestMethod]
        public void Test_Straight_Start_1()
        {
            var straight = new List<Dice>
            {
                orderedDice[0],
                orderedDice[1],
                orderedDice[2],
                orderedDice[3],
                orderedDice[4]
            };
            Assert.IsTrue(straightRule.PassesRule(straight));
        }

        [TestMethod]
        public void Test_Straight_Start_2()
        {
            var straight = new List<Dice>
            {
                orderedDice[1],
                orderedDice[2],
                orderedDice[3],
                orderedDice[4],
                orderedDice[5],
            };
            Assert.IsTrue(straightRule.PassesRule(straight));
        }

        [TestMethod]
        public void Test_Straight_None()
        {
            var noStraight = new List<Dice>
            {
                orderedDice[0],
                orderedDice[2],
                orderedDice[3],
                orderedDice[4],
                orderedDice[5],
            };
            Assert.IsFalse(straightRule.PassesRule(noStraight));
        }

        [TestMethod]
        public void Test_Straight_TooShort()
        {
            var shortStraight = new List<Dice>
            {
                orderedDice[1],
                orderedDice[2],
                orderedDice[3],
                orderedDice[3],
                orderedDice[4],
            };
            Assert.IsFalse(straightRule.PassesRule(shortStraight));
        }

        [TestMethod]
        public void Test_Straight_One_Die()
        {
            var straight = new List<Dice>
            {
                orderedDice[1],
            };
            Assert.IsTrue(straightRule.PassesRule(straight));
        }

        #endregion

        #region Kind Tests

        [TestMethod]
        public void Test_Kind_Short()
        {
            var rule = new KindRule(3);
            var dice = new List<Dice>
            {
                orderedDice[1],
                orderedDice[1]
            };
            Assert.IsFalse(rule.PassesRule(dice));
        }

        [TestMethod]
        public void Test_Kind()
        {
            var rule = new KindRule(4);
            var dice = new List<Dice>
            {
                orderedDice[2],
                orderedDice[2],
                orderedDice[2],
                orderedDice[2]
            };
            Assert.IsTrue(rule.PassesRule(dice));
        }

        [TestMethod]
        public void Test_Kind_Not()
        {
            var rule = new KindRule(5);
            var dice = new List<Dice>
            {
                orderedDice[3],
                orderedDice[3],
                orderedDice[3],
                orderedDice[3],
                orderedDice[5]
            };
            Assert.IsFalse(rule.PassesRule(dice));
        }

        #endregion
    }
}