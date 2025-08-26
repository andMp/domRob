using Nethereum.Web3;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Numerics;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.Contracts;


public class EthereumService
{
    private readonly Web3 web3;
    private readonly string contractAddress = "0x9335c1Aa0c4552a14B7A9add1185C4411ad4FF5C";
    private readonly Nethereum.Contracts.Contract contract;

    private const string abi = @"[
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""addr"",
				""type"": ""address""
			}
		],
		""name"": ""blockAddress"",
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
		""inputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""constructor""
	},
	{
		""anonymous"": false,
		""inputs"": [
			{
				""indexed"": false,
				""internalType"": ""address"",
				""name"": ""addr"",
				""type"": ""address""
			}
		],
		""name"": ""Blocked"",
		""type"": ""event""
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
			},
			{
				""indexed"": false,
				""internalType"": ""uint256"",
				""name"": ""time"",
				""type"": ""uint256""
			}
		],
		""name"": ""Tipped"",
		""type"": ""event""
	},
	{
		""inputs"": [
			{
				""internalType"": ""address"",
				""name"": ""addr"",
				""type"": ""address""
			}
		],
		""name"": ""unblockAddress"",
		""outputs"": [],
		""stateMutability"": ""nonpayable"",
		""type"": ""function""
	},
	{
		""anonymous"": false,
		""inputs"": [
			{
				""indexed"": false,
				""internalType"": ""address"",
				""name"": ""addr"",
				""type"": ""address""
			}
		],
		""name"": ""Unblocked"",
		""type"": ""event""
	},
	{
		""inputs"": [],
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
				""internalType"": ""uint256"",
				""name"": ""n"",
				""type"": ""uint256""
			}
		],
		""name"": ""lastTips"",
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
						""name"": ""time"",
						""type"": ""uint256""
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
				""internalType"": ""address"",
				""name"": """",
				""type"": ""address""
			}
		],
		""name"": ""rating"",
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
				""name"": ""time"",
				""type"": ""uint256""
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
        [Nethereum.ABI.FunctionEncoding.Attributes.Parameter("address", "from", 1)]
        public string From { get; set; }

        [Nethereum.ABI.FunctionEncoding.Attributes.Parameter("uint256", "amount", 2)]
        public BigInteger Amount { get; set; }

        [Nethereum.ABI.FunctionEncoding.Attributes.Parameter("string", "message", 3)]
        public string Message { get; set; }

        [Nethereum.ABI.FunctionEncoding.Attributes.Parameter("uint256", "time", 4)]
        public BigInteger Time { get; set; }
    }

    public async Task<BigInteger> GetTotalTipsAsync()
    {
        var function = contract.GetFunction("totalTips");
        return await function.CallAsync<BigInteger>();
    }

    public async Task<List<TipDTO>> GetLastTipsAsync(int n)
    {
        var function = contract.GetFunction("lastTips");
        return await function.CallAsync<List<TipDTO>>(n);
    }
}
