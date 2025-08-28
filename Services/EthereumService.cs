using Nethereum.Web3;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;

public class EthereumService
{
    private readonly Web3 web3;
    private readonly string contractAddress = "0x7DFf8E84B1e0e6bc52B088FAAf45F4e05d13bFE0";
    private readonly Nethereum.Contracts.Contract contract;

    private const string abi = @"[
	{
		""inputs"": [
			{
				""internalType"": ""uint256"",
				""name"": ""tipIndex"",
				""type"": ""uint256""
			},
			{
				""internalType"": ""string"",
				""name"": ""feedback_"",
				""type"": ""string""
			}
		],
		""name"": ""addFeedback"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""inputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""constructor""
	},
	{
		""anonymous"": false,
		""inputs"": [
			{
				""indexed"": true,
				""internalType"": ""address"",
				""name"": ""user"",
				""type"": ""address""
			}
		],
		""name"": ""AddressBlocked"",
		""type"": ""event""
	},
	{
		""anonymous"": false,
		""inputs"": [
			{
				""indexed"": true,
				""internalType"": ""address"",
				""name"": ""user"",
				""type"": ""address""
			}
		],
		""name"": ""AddressUnblocked"",
		""type"": ""event""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""user"",
				""type"": ""address""
			}
		],
		""name"": ""blockAddress"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""anonymous"": false,
		""inputs"": [
			{
				""indexed"": true,
				""internalType"": ""address"",
				""name"": ""from"",
				""type"": ""address""
			},
			{
				""indexed"": false,
				""internalType"": ""uint256"",
				""name"": ""tipIndex"",
				""type"": ""uint256""
			},
			{
				""indexed"": false,
				""internalType"": ""string"",
				""name"": ""feedback"",
				""type"": ""string""
			}
		],
		""name"": ""FeedbackAdded"",
		""type"": ""event""
	},
	{
		""anonymous"": false,
		""inputs"": [
			{
				""indexed"": false,
				""internalType"": ""uint256"",
				""name"": ""tipIndex"",
				""type"": ""uint256""
			},
			{
				""indexed"": false,
				""internalType"": ""string"",
				""name"": ""reply"",
				""type"": ""string""
			}
		],
		""name"": ""OwnerReplied"",
		""type"": ""event""
	},
	{
		""inputs"": [
			{
				""internalType"": ""uint256"",
				""name"": ""tipIndex"",
				""type"": ""uint256""
			},
			{
				""internalType"": ""string"",
				""name"": ""reply_"",
				""type"": ""string""
			}
		],
		""name"": ""replyFeedback"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""string"",
				""name"": ""message_"",
				""type"": ""string""
			}
		],
		""name"": ""tip"",
		""outputs"": [],
		""stateMutability"": ""payable"",
		""type"": ""function""
	},
	{
		""anonymous"": false,
		""inputs"": [
			{
				""indexed"": true,
				""internalType"": ""address"",
				""name"": ""from"",
				""type"": ""address""
			},
			{
				""indexed"": false,
				""internalType"": ""uint256"",
				""name"": ""amount"",
				""type"": ""uint256""
			},
			{
				""indexed"": false,
				""internalType"": ""string"",
				""name"": ""message"",
				""type"": ""string""
			}
		],
		""name"": ""Tipped"",
		""type"": ""event""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""user"",
				""type"": ""address""
			}
		],
		""name"": ""unblockAddress"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""uint256"",
				""name"": ""amount"",
				""type"": ""uint256""
			}
		],
		""name"": ""withdraw"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""anonymous"": false,
		""inputs"": [
			{
				""indexed"": true,
				""internalType"": ""address"",
				""name"": ""to"",
				""type"": ""address""
			},
			{
				""indexed"": false,
				""internalType"": ""uint256"",
				""name"": ""amount"",
				""type"": ""uint256""
			}
		],
		""name"": ""Withdrawn"",
		""type"": ""event""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": """",
				""type"": ""address""
			}
		],
		""name"": ""blocked"",
		""outputs"": [
			{
				""internalType"": ""bool"",
				""name"": """",
				""type"": ""bool""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [],
		""name"": ""contractBalance"",
		""outputs"": [
			{
				""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": """",
				""type"": ""address""
			}
		],
		""name"": ""donorTotals"",
		""outputs"": [
			{
				""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""uint256"",
				""name"": ""n"",
				""type"": ""uint256""
			}
		],
		""name"": ""lastNTips"",
		""outputs"": [
			{
				""components"": [
					{
						""internalType"": ""address"",
						""name"": ""from"",
						""type"": ""address""
					},
					{
						""internalType"": ""uint256"",
						""name"": ""amount"",
						""type"": ""uint256""
					},
					{
						""internalType"": ""string"",
						""name"": ""message"",
						""type"": ""string""
					},
					{
						""internalType"": ""uint256"",
						""name"": ""timestamp"",
						""type"": ""uint256""
					},
					{
						""internalType"": ""string"",
						""name"": ""feedback"",
						""type"": ""string""
					},
					{
						""internalType"": ""string"",
						""name"": ""ownerReply"",
						""type"": ""string""
					}
				],
				""internalType"": ""struct TipJar.Tip[]"",
				""name"": """",
				""type"": ""tuple[]""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [],
		""name"": ""MESSAGE_MAX_LENGTH"",
		""outputs"": [
			{
				""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [],
		""name"": ""MIN_TIP"",
		""outputs"": [
			{
				""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [],
		""name"": ""owner"",
		""outputs"": [
			{
				""internalType"": ""address"",
				""name"": """",
				""type"": ""address""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			}
		],
		""name"": ""tips"",
		""outputs"": [
			{
				""internalType"": ""address"",
				""name"": ""from"",
				""type"": ""address""
			},
			{
				""internalType"": ""uint256"",
				""name"": ""amount"",
				""type"": ""uint256""
			},
			{
				""internalType"": ""string"",
				""name"": ""message"",
				""type"": ""string""
			},
			{
				""internalType"": ""uint256"",
				""name"": ""timestamp"",
				""type"": ""uint256""
			},
			{
				""internalType"": ""string"",
				""name"": ""feedback"",
				""type"": ""string""
			},
			{
				""internalType"": ""string"",
				""name"": ""ownerReply"",
				""type"": ""string""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [
			{
				""internalType"": ""uint256"",
				""name"": ""topN"",
				""type"": ""uint256""
			}
		],
		""name"": ""topDonors"",
		""outputs"": [
			{
				""internalType"": ""address[]"",
				""name"": """",
				""type"": ""address[]""
			},
			{
				""internalType"": ""uint256[]"",
				""name"": """",
				""type"": ""uint256[]""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	},
	{
		""inputs"": [],
		""name"": ""totalTips"",
		""outputs"": [
			{
				""internalType"": ""uint256"",
				""name"": """",
				""type"": ""uint256""
			}
		],
		""stateMutability"": ""view"",
		""type"": ""function""
	}
]";

    public EthereumService(string rpcUrl)
    {
        web3 = new Web3(rpcUrl);
        contract = web3.Eth.GetContract(abi, contractAddress);
    }

    [FunctionOutput]
    public class TipDTO
    {
        [Parameter("address", "from", 1)]
        public string From { get; set; }

        [Parameter("uint256", "amount", 2)]
        public BigInteger Amount { get; set; }

        [Parameter("string", "message", 3)]
        public string Message { get; set; }

        [Parameter("uint256", "time", 4)]
        public BigInteger Time { get; set; }
    }

    public async Task<BigInteger> GetTotalTipsAsync()
    {
        var f = contract.GetFunction("totalTips");
        return await f.CallAsync<BigInteger>();
    }

    public async Task<List<TipDTO>> GetLastTipsAsync(int n)
    {
        var f = contract.GetFunction("lastTips");
        return await f.CallAsync<List<TipDTO>>(n);
    }

    public async Task<bool> GetPausedAsync()
    {
        var f = contract.GetFunction("paused");
        return await f.CallAsync<bool>();
    }

    public async Task<BigInteger> GetMinTipAsync()
    {
        var f = contract.GetFunction("minTip");
        return await f.CallAsync<BigInteger>();
    }

    public async Task<BigInteger> GetCooldownAsync()
    {
        var f = contract.GetFunction("cooldown");
        return await f.CallAsync<BigInteger>();
    }

    public async Task<string> GetOwnerAsync()
    {
        var f = contract.GetFunction("owner");
        return await f.CallAsync<string>();
    }
    public async Task<List<(string Address, decimal Total)>> GetTopDonorsAsync(int topN)
    {
        var f = contract.GetFunction("topDonors");
        var result = await f.CallDeserializingToObjectAsync<TopDonorsDTO>(topN);
        var list = result.Addresses.Zip(result.Amounts, (addr, amt) => (addr, (decimal)amt / 1e18m)).ToList();
        return list;
    }

    [FunctionOutput]
    public class TopDonorsDTO
    {
        [Parameter("address[]", "addresses", 1)]
        public List<string> Addresses { get; set; }

        [Parameter("uint256[]", "amounts", 2)]
        public List<BigInteger> Amounts { get; set; }
    }
}
