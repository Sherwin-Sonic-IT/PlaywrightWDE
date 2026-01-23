
using System;

namespace PlaywrightWDE.Credentials
{
    public enum AccountType { FSS, HPC, FIGL }

    public record Account(
        AccountType Type,
        string Username,
        string FirstPassword,
        string SecondPassword
    );

    public static class Accounts
    {
        public static Account Resolve(string[] args) =>
            Enum.TryParse(
                args.Length > 0 ? args[0] : nameof(AccountType.FSS),
                true,
                out AccountType type
            )
                ? Create(type)
                : Create(AccountType.FSS);

        private static Account Create(AccountType type) => type switch
        {
            AccountType.HPC =>
                new(type, "4048SONS09", "ITEM_hpc@1127", "It@110625"),

            AccountType.FIGL =>
                new(type, "4048SONFI2", "INVacc#26@2025", "SONic1#@2026"),

            AccountType.FSS or _ =>
                new(type, "4048sons10", "ITS&pss24@004", "ITS&pss24@16")
        };
    }
}







