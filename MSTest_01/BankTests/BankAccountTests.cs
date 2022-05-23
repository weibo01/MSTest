using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;//向测试类中添加 using 语句，供测试项目调用

/*
验证行为：
    （1）如果借方金额有效（借方金额小于余额且大于零），该方法会从帐户余额中减去该借方金额。
    （2）如果借方金额小于零，该方法会引发 ArgumentOutOfRangeException。
    （3）如果借方金额大于余额，该方法将引发 ArgumentOutOfRangeException 。
    
    
*/

/*
对测试类[TestClass]的最低要求：
    （1）任何包含要在“测试资源管理器”中运行的单元测试方法的类都需要有 [TestClass] 特性。
    （2）需要“测试资源管理器”识别的每个测试方法都必须具有 [TestMethod] 属性。

对测试方法[TestMethod]的要求：
    （1）使用 [TestMethod] 特性进行修饰
    （2）它将返回 void
    （3）它不能含有参数
*/

namespace BankTests
{
    [TestClass]
    public class BankAccountTests
    {
        //（1）验证借方金额小于余额且大于零的行为
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange（准备: 执行测试所需要的设置和初始化）
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act（行动: 采取测试所需的行动）
            account.Debit(debitAmount);

            // Assert（断言: 验证测试结果）
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        //（2）验证借方金额小于零时的行为
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act and assert
            Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => account.Debit(debitAmount));
        }

        //（3）验证借方金额大于余额的行为
        [TestMethod]
        public void Debit_WhenAmountIsMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // Arrange
            double beginningBalance = 11.99;
            double debitAmount = 20.0;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act
            try
            {
                account.Debit(debitAmount);
            }
            catch (System.ArgumentOutOfRangeException e)
            {
                // Assert
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }
    }
}