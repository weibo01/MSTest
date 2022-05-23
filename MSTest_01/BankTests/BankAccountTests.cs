using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountNS;//������������ using ��䣬��������Ŀ����

/*
��֤��Ϊ��
    ��1������跽�����Ч���跽���С������Ҵ����㣩���÷�������ʻ�����м�ȥ�ý跽��
    ��2������跽���С���㣬�÷��������� ArgumentOutOfRangeException��
    ��3������跽���������÷��������� ArgumentOutOfRangeException ��
    
    
*/

/*
�Բ�����[TestClass]�����Ҫ��
    ��1���κΰ���Ҫ�ڡ�������Դ�������������еĵ�Ԫ���Է������඼��Ҫ�� [TestClass] ���ԡ�
    ��2����Ҫ��������Դ��������ʶ���ÿ�����Է������������ [TestMethod] ���ԡ�

�Բ��Է���[TestMethod]��Ҫ��
    ��1��ʹ�� [TestMethod] ���Խ�������
    ��2���������� void
    ��3�������ܺ��в���
*/

namespace BankTests
{
    [TestClass]
    public class BankAccountTests
    {
        //��1����֤�跽���С������Ҵ��������Ϊ
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // Arrange��׼��: ִ�в�������Ҫ�����úͳ�ʼ����
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // Act���ж�: ��ȡ����������ж���
            account.Debit(debitAmount);

            // Assert������: ��֤���Խ����
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }

        //��2����֤�跽���С����ʱ����Ϊ
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

        //��3����֤�跽������������Ϊ
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