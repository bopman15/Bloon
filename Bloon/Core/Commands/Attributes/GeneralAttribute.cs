namespace Bloon.Core.Commands.Attributes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Bloon.Variables.Channels;
    using DSharpPlus.CommandsNext;
    using DSharpPlus.CommandsNext.Attributes;

    /**
     * <summary>
     * Precondition for general commands.
     * </summary>
     */
    public class GeneralAttribute : CheckBaseAttribute
    {
        public override Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            List<ulong> availChannels = new List<ulong>
            {
                SBGChannels.Help,
                SBGChannels.General,
                SBGChannels.Mapmaker,
                SBGChannels.TradingPost,
                SBGChannels.CompetitiveMatches,
                SBGChannels.AUG,
                SBGChannels.SecretBaseAlpha,
                SBGChannels.Wiki,
                SBGChannels.Offtopic,
                BloonChannels.CommandCentre,
                BloonChannels.Commands,
                BloonChannels.Ground0,
            };
            if (availChannels.Contains(ctx.Channel.Id) || ctx.Channel.IsPrivate)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
