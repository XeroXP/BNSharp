﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BNSharp.Tests
{
    internal class Fixtures
    {
        public static Dictionary<string, Dictionary<string, string>> DhGroups = new()
        {
            {
                "p16",
                new Dictionary<string, string>()
                {
                    {
                        "prime",
                        "ffffffffffffffffc90fdaa22168c234c4c6628b80dc1cd1" +
                            "29024e088a67cc74020bbea63b139b22514a08798e3404dd" +
                            "ef9519b3cd3a431b302b0a6df25f14374fe1356d6d51c245" +
                            "e485b576625e7ec6f44c42e9a637ed6b0bff5cb6f406b7ed" +
                            "ee386bfb5a899fa5ae9f24117c4b1fe649286651ece45b3d" +
                            "c2007cb8a163bf0598da48361c55d39a69163fa8fd24cf5f" +
                            "83655d23dca3ad961c62f356208552bb9ed529077096966d" +
                            "670c354e4abc9804f1746c08ca18217c32905e462e36ce3b" +
                            "e39e772c180e86039b2783a2ec07a28fb5c55df06f4c52c9" +
                            "de2bcbf6955817183995497cea956ae515d2261898fa0510" +
                            "15728e5a8aaac42dad33170d04507a33a85521abdf1cba64" +
                            "ecfb850458dbef0a8aea71575d060c7db3970f85a6e1e4c7" +
                            "abf5ae8cdb0933d71e8c94e04a25619dcee3d2261ad2ee6b" +
                            "f12ffa06d98a0864d87602733ec86a64521f2b18177b200c" +
                            "bbe117577a615d6c770988c0bad946e208e24fa074e5ab31" +
                            "43db5bfce0fd108e4b82d120a92108011a723c12a787e6d7" +
                            "88719a10bdba5b2699c327186af4e23c1a946834b6150bda" +
                            "2583e9ca2ad44ce8dbbbc2db04de8ef92e8efc141fbecaa6" +
                            "287c59474e6bc05d99b2964fa090c3a2233ba186515be7ed" +
                            "1f612970cee2d7afb81bdd762170481cd0069127d5b05aa9" +
                            "93b4ea988d8fddc186ffb7dc90a6c08f4df435c934063199" +
                            "ffffffffffffffff"
                    },
                    {
                        "priv",
                        "6d5923e6449122cbbcc1b96093e0b7e4fd3e469f58daddae" +
                            "53b49b20664f4132675df9ce98ae0cfdcac0f4181ccb643b" +
                            "625f98104dcf6f7d8e81961e2cab4b5014895260cb977c7d" +
                            "2f981f8532fb5da60b3676dfe57f293f05d525866053ac7e" +
                            "65abfd19241146e92e64f309a97ef3b529af4d6189fa416c" +
                            "9e1a816c3bdf88e5edf48fbd8233ef9038bb46faa95122c0" +
                            "5a426be72039639cd2d53d37254b3d258960dcb33c255ede" +
                            "20e9d7b4b123c8b4f4b986f53cdd510d042166f7dd7dca98" +
                            "7c39ab36381ba30a5fdd027eb6128d2ef8e5802a2194d422" +
                            "b05fe6e1cb4817789b923d8636c1ec4b7601c90da3ddc178" +
                            "52f59217ae070d87f2e75cbfb6ff92430ad26a71c8373452" +
                            "ae1cc5c93350e2d7b87e0acfeba401aaf518580937bf0b6c" +
                            "341f8c49165a47e49ce50853989d07171c00f43dcddddf72" +
                            "94fb9c3f4e1124e98ef656b797ef48974ddcd43a21fa06d0" +
                            "565ae8ce494747ce9e0ea0166e76eb45279e5c6471db7df8" +
                            "cc88764be29666de9c545e72da36da2f7a352fb17bdeb982" +
                            "a6dc0193ec4bf00b2e533efd6cd4d46e6fb237b775615576" +
                            "dd6c7c7bbc087a25e6909d1ebc6e5b38e5c8472c0fc429c6" +
                            "f17da1838cbcd9bbef57c5b5522fd6053e62ba21fe97c826" +
                            "d3889d0cc17e5fa00b54d8d9f0f46fb523698af965950f4b" +
                            "941369e180f0aece3870d9335f2301db251595d173902cad" +
                            "394eaa6ffef8be6c"
                    },
                    {
                        "pub",
                        "d53703b7340bc89bfc47176d351e5cf86d5a18d9662eca3c" +
                            "9759c83b6ccda8859649a5866524d77f79e501db923416ca" +
                            "2636243836d3e6df752defc0fb19cc386e3ae48ad647753f" +
                            "bf415e2612f8a9fd01efe7aca249589590c7e6a0332630bb" +
                            "29c5b3501265d720213790556f0f1d114a9e2071be3620bd" +
                            "4ee1e8bb96689ac9e226f0a4203025f0267adc273a43582b" +
                            "00b70b490343529eaec4dcff140773cd6654658517f51193" +
                            "13f21f0a8e04fe7d7b21ffeca85ff8f87c42bb8d9cb13a72" +
                            "c00e9c6e9dfcedda0777af951cc8ccab90d35e915e707d8e" +
                            "4c2aca219547dd78e9a1a0730accdc9ad0b854e51edd1e91" +
                            "4756760bab156ca6e3cb9c625cf0870def34e9ac2e552800" +
                            "d6ce506d43dbbc75acfa0c8d8fb12daa3c783fb726f187d5" +
                            "58131779239c912d389d0511e0f3a81969d12aeee670e48f" +
                            "ba41f7ed9f10705543689c2506b976a8ffabed45e33795b0" +
                            "1df4f6b993a33d1deab1316a67419afa31fbb6fdd252ee8c" +
                            "7c7d1d016c44e3fcf6b41898d7f206aa33760b505e4eff2e" +
                            "c624bc7fe636b1d59e45d6f904fc391419f13d1f0cdb5b6c" +
                            "2378b09434159917dde709f8a6b5dc30994d056e3f964371" +
                            "11587ac7af0a442b8367a7bd940f752ddabf31cf01171e24" +
                            "d78df136e9681cd974ce4f858a5fb6efd3234a91857bb52d" +
                            "9e7b414a8bc66db4b5a73bbeccfb6eb764b4f0cbf0375136" +
                            "b024b04e698d54a5"
                    }
                }
            },
            {
                "p17",
                new Dictionary<string, string>()
                {
                    {
                        "prime",
                        "ffffffffffffffffc90fdaa22168c234c4c6628b80dc1cd1" +
                            "29024e088a67cc74020bbea63b139b22514a08798e3404dd" +
                            "ef9519b3cd3a431b302b0a6df25f14374fe1356d6d51c245" +
                            "e485b576625e7ec6f44c42e9a637ed6b0bff5cb6f406b7ed" +
                            "ee386bfb5a899fa5ae9f24117c4b1fe649286651ece45b3d" +
                            "c2007cb8a163bf0598da48361c55d39a69163fa8fd24cf5f" +
                            "83655d23dca3ad961c62f356208552bb9ed529077096966d" +
                            "670c354e4abc9804f1746c08ca18217c32905e462e36ce3b" +
                            "e39e772c180e86039b2783a2ec07a28fb5c55df06f4c52c9" +
                            "de2bcbf6955817183995497cea956ae515d2261898fa0510" +
                            "15728e5a8aaac42dad33170d04507a33a85521abdf1cba64" +
                            "ecfb850458dbef0a8aea71575d060c7db3970f85a6e1e4c7" +
                            "abf5ae8cdb0933d71e8c94e04a25619dcee3d2261ad2ee6b" +
                            "f12ffa06d98a0864d87602733ec86a64521f2b18177b200c" +
                            "bbe117577a615d6c770988c0bad946e208e24fa074e5ab31" +
                            "43db5bfce0fd108e4b82d120a92108011a723c12a787e6d7" +
                            "88719a10bdba5b2699c327186af4e23c1a946834b6150bda" +
                            "2583e9ca2ad44ce8dbbbc2db04de8ef92e8efc141fbecaa6" +
                            "287c59474e6bc05d99b2964fa090c3a2233ba186515be7ed" +
                            "1f612970cee2d7afb81bdd762170481cd0069127d5b05aa9" +
                            "93b4ea988d8fddc186ffb7dc90a6c08f4df435c934028492" +
                            "36c3fab4d27c7026c1d4dcb2602646dec9751e763dba37bd" +
                            "f8ff9406ad9e530ee5db382f413001aeb06a53ed9027d831" +
                            "179727b0865a8918da3edbebcf9b14ed44ce6cbaced4bb1b" +
                            "db7f1447e6cc254b332051512bd7af426fb8f401378cd2bf" +
                            "5983ca01c64b92ecf032ea15d1721d03f482d7ce6e74fef6" +
                            "d55e702f46980c82b5a84031900b1c9e59e7c97fbec7e8f3" +
                            "23a97a7e36cc88be0f1d45b7ff585ac54bd407b22b4154aa" +
                            "cc8f6d7ebf48e1d814cc5ed20f8037e0a79715eef29be328" +
                            "06a1d58bb7c5da76f550aa3d8a1fbff0eb19ccb1a313d55c" +
                            "da56c9ec2ef29632387fe8d76e3c0468043e8f663f4860ee" +
                            "12bf2d5b0b7474d6e694f91e6dcc4024ffffffffffffffff"
                    },
                    {
                        "priv",
                        "6017f2bc23e1caff5b0a8b4e1fc72422b5204415787801dc" +
                            "025762b8dbb98ab57603aaaa27c4e6bdf742b4a1726b9375" +
                            "a8ca3cf07771779589831d8bd18ddeb79c43e7e77d433950" +
                            "e652e49df35b11fa09644874d71d62fdaffb580816c2c88c" +
                            "2c4a2eefd4a660360316741b05a15a2e37f236692ad3c463" +
                            "fff559938fc6b77176e84e1bb47fb41af691c5eb7bb81bd8" +
                            "c918f52625a1128f754b08f5a1403b84667231c4dfe07ed4" +
                            "326234c113931ce606037e960f35a2dfdec38a5f057884d3" +
                            "0af8fab3be39c1eeb390205fd65982191fc21d5aa30ddf51" +
                            "a8e1c58c0c19fc4b4a7380ea9e836aaf671c90c29bc4bcc7" +
                            "813811aa436a7a9005de9b507957c56a9caa1351b6efc620" +
                            "7225a18f6e97f830fb6a8c4f03b82f4611e67ab9497b9271" +
                            "d6ac252793cc3e5538990dbd894d2dbc2d152801937d9f74" +
                            "da4b741b50b4d40e4c75e2ac163f7b397fd555648b249f97" +
                            "ffe58ffb6d096aa84534c4c5729cff137759bd34e80db4ab" +
                            "47e2b9c52064e7f0bf677f72ac9e5d0c6606943683f9d12f" +
                            "180cf065a5cb8ec3179a874f358847a907f8471d15f1e728" +
                            "7023249d6d13c82da52628654438f47b8b5cdf4761fbf6ad" +
                            "9219eceac657dbd06cf2ab776ad4c968f81c3d039367f0a4" +
                            "d77c7ec4435c27b6c147071665100063b5666e06eb2fb2cc" +
                            "3159ba34bc98ca346342195f6f1fb053ddc3bc1873564d40" +
                            "1c6738cdf764d6e1ff25ca5926f80102ea6593c17170966b" +
                            "b5d7352dd7fb821230237ea3ebed1f920feaadbd21be295a" +
                            "69f2083deae9c5cdf5f4830eb04b7c1f80cc61c17232d79f" +
                            "7ecc2cc462a7965f804001c89982734e5abba2d31df1b012" +
                            "152c6b226dff34510b54be8c2cd68d795def66c57a3abfb6" +
                            "896f1d139e633417f8c694764974d268f46ece3a8d6616ea" +
                            "a592144be48ee1e0a1595d3e5edfede5b27cec6c48ceb2ff" +
                            "b42cb44275851b0ebf87dfc9aa2d0cb0805e9454b051dfe8" +
                            "a29fadd82491a4b4c23f2d06ba45483ab59976da1433c9ce" +
                            "500164b957a04cf62dd67595319b512fc4b998424d1164dd" +
                            "bbe5d1a0f7257cbb04ec9b5ed92079a1502d98725023ecb2"
                    },
                    {
                        "pub",
                        "3bf836229c7dd874fe37c1790d201e82ed8e192ed61571ca" +
                            "7285264974eb2a0171f3747b2fc23969a916cbd21e14f7e2" +
                            "f0d72dcd2247affba926f9e7bb99944cb5609aed85e71b89" +
                            "e89d2651550cb5bd8281bd3144066af78f194032aa777739" +
                            "cccb7862a1af401f99f7e5c693f25ddce2dedd9686633820" +
                            "d28d0f5ed0c6b5a094f5fe6170b8e2cbc9dff118398baee6" +
                            "e895a6301cb6e881b3cae749a5bdf5c56fc897ff68bc73f2" +
                            "4811bb108b882872bade1f147d886a415cda2b93dd90190c" +
                            "be5c2dd53fe78add5960e97f58ff2506afe437f4cf4c912a" +
                            "397c1a2139ac6207d3ab76e6b7ffd23bb6866dd7f87a9ae5" +
                            "578789084ff2d06ea0d30156d7a10496e8ebe094f5703539" +
                            "730f5fdbebc066de417be82c99c7da59953071f49da7878d" +
                            "a588775ff2a7f0084de390f009f372af75cdeba292b08ea8" +
                            "4bd13a87e1ca678f9ad148145f7cef3620d69a891be46fbb" +
                            "cad858e2401ec0fd72abdea2f643e6d0197b7646fbb83220" +
                            "0f4cf7a7f6a7559f9fb0d0f1680822af9dbd8dec4cd1b5e1" +
                            "7bc799e902d9fe746ddf41da3b7020350d3600347398999a" +
                            "baf75d53e03ad2ee17de8a2032f1008c6c2e6618b62f225b" +
                            "a2f350179445debe68500fcbb6cae970a9920e321b468b74" +
                            "5fb524fb88abbcacdca121d737c44d30724227a99745c209" +
                            "b970d1ff93bbc9f28b01b4e714d6c9cbd9ea032d4e964d8e" +
                            "8fff01db095160c20b7646d9fcd314c4bc11bcc232aeccc0" +
                            "fbedccbc786951025597522eef283e3f56b44561a0765783" +
                            "420128638c257e54b972a76e4261892d81222b3e2039c61a" +
                            "ab8408fcaac3d634f848ab3ee65ea1bd13c6cd75d2e78060" +
                            "e13cf67fbef8de66d2049e26c0541c679fff3e6afc290efe" +
                            "875c213df9678e4a7ec484bc87dae5f0a1c26d7583e38941" +
                            "b7c68b004d4df8b004b666f9448aac1cc3ea21461f41ea5d" +
                            "d0f7a9e6161cfe0f58bcfd304bdc11d78c2e9d542e86c0b5" +
                            "6985cc83f693f686eaac17411a8247bf62f5ccc7782349b5" +
                            "cc1f20e312fa2acc0197154d1bfee507e8db77e8f2732f2d" +
                            "641440ccf248e8643b2bd1e1f9e8239356ab91098fcb431d"
                    },
                    {
                        "q",
                        "a899c59999bf877d96442d284359783bdc64b5f878b688fe" +
                            "51407f0526e616553ad0aaaac4d5bed3046f10a1faaf42bb" +
                            "2342dc4b7908eea0c46e4c4576897675c2bfdc4467870d3d" +
                            "cd90adaed4359237a4bc6924bfb99aa6bf5f5ede15b574ea" +
                            "e977eac096f3c67d09bda574c6306c6123fa89d2f086b8dc" +
                            "ff92bc570c18d83fe6c810ccfd22ce4c749ef5e6ead3fffe" +
                            "c63d95e0e3fde1df9db6a35fa1d107058f37e41957769199" +
                            "d945dd7a373622c65f0af3fd9eb1ddc5c764bbfaf7a3dc37" +
                            "2548e683b970dac4aa4b9869080d2376c9adecebb84e172c" +
                            "09aeeb25fb8df23e60033260c4f8aac6b8b98ab894b1fb84" +
                            "ebb83c0fb2081c3f3eee07f44e24d8fabf76f19ed167b0d7" +
                            "ff971565aa4efa3625fce5a43ceeaa3eebb3ce88a00f597f" +
                            "048c69292b38dba2103ecdd5ec4ccfe3b2d87fa6202f334b" +
                            "c1cab83b608dfc875b650b69f2c7e23c0b2b4adf149a6100" +
                            "db1b6dbad4679ecb1ea95eafaba3bd00db11c2134f5a8686" +
                            "358b8b2ab49a1b2e85e1e45caeac5cd4dc0b3b5fffba8871" +
                            "1c6baf399edd48dad5e5c313702737a6dbdcede80ca358e5" +
                            "1d1c4fe42e8948a084403f61baed38aa9a1a5ce2918e9f33" +
                            "100050a430b47bc592995606440272a4994677577a6aaa1b" +
                            "a101045dbec5a4e9566dab5445d1af3ed19519f07ac4e2a8" +
                            "bd0a84b01978f203a9125a0be020f71fab56c2c9e344d4f4" +
                            "12d53d3cd8eb74ca5122002e931e3cb0bd4b7492436be17a" +
                            "d7ebe27148671f59432c36d8c56eb762655711cfc8471f70" +
                            "83a8b7283bcb3b1b1d47d37c23d030288cfcef05fbdb4e16" +
                            "652ee03ee7b77056a808cd700bc3d9ef826eca9a59be959c" +
                            "947c865d6b372a1ca2d503d7df6d7611b12111665438475a" +
                            "1c64145849b3da8c2d343410df892d958db232617f9896f1" +
                            "de95b8b5a47132be80dd65298c7f2047858409bf762dbc05" +
                            "a62ca392ac40cfb8201a0607a2cae07d99a307625f2b2d04" +
                            "fe83fbd3ab53602263410f143b73d5b46fc761882e78c782" +
                            "d2c36e716a770a7aefaf7f76cea872db7bffefdbc4c2f9e0" +
                            "39c19adac915e7a63dcb8c8c78c113f29a3e0bc10e100ce0"
                    },
                    {
                        "qs",
                        "6f0a2fb763eaeb8eb324d564f03d4a55fdcd709e5f1b65e9" +
                            "5702b0141182f9f945d71bc3e64a7dfdae7482a7dd5a4e58" +
                            "bc38f78de2013f2c468a621f08536969d2c8d011bb3bc259" +
                            "2124692c91140a5472cad224acdacdeae5751dadfdf068b8" +
                            "77bfa7374694c6a7be159fc3d24ff9eeeecaf62580427ad8" +
                            "622d48c51a1c4b1701d768c79d8c819776e096d2694107a2" +
                            "f3ec0c32224795b59d32894834039dacb369280afb221bc0" +
                            "90570a93cf409889b818bb30cccee98b2aa26dbba0f28499" +
                            "08e1a3cd43fa1f1fb71049e5c77c3724d74dc351d9989057" +
                            "37bbda3805bd6b1293da8774410fb66e3194e18cdb304dd9" +
                            "a0b59b583dcbc9fc045ac9d56aea5cfc9f8a0b95da1e11b7" +
                            "574d1f976e45fe12294997fac66ca0b83fc056183549e850" +
                            "a11413cc4abbe39a211e8c8cbf82f2a23266b3c10ab9e286" +
                            "07a1b6088909cddff856e1eb6b2cde8bdac53fa939827736" +
                            "ca1b892f6c95899613442bd02dbdb747f02487718e2d3f22" +
                            "f73734d29767ed8d0e346d0c4098b6fdcb4df7d0c4d29603" +
                            "5bffe80d6c65ae0a1b814150d349096baaf950f2caf298d2" +
                            "b292a1d48cf82b10734fe8cedfa16914076dfe3e9b51337b" +
                            "ed28ea1e6824bb717b641ca0e526e175d3e5ed7892aebab0" +
                            "f207562cc938a821e2956107c09b6ce4049adddcd0b7505d" +
                            "49ae6c69a20122461102d465d93dc03db026be54c303613a" +
                            "b8e5ce3fd4f65d0b6162ff740a0bf5469ffd442d8c509cd2" +
                            "3b40dab90f6776ca17fc0678774bd6eee1fa85ababa52ec1" +
                            "a15031eb677c6c488661dddd8b83d6031fe294489ded5f08" +
                            "8ad1689a14baeae7e688afa3033899c81f58de39b392ca94" +
                            "af6f15a46f19fa95c06f9493c8b96a9be25e78b9ea35013b" +
                            "caa76de6303939299d07426a88a334278fc3d0d9fa71373e" +
                            "be51d3c1076ab93a11d3d0d703366ff8cde4c11261d488e5" +
                            "60a2bdf3bfe2476032294800d6a4a39d306e65c6d7d8d66e" +
                            "5ec63eee94531e83a9bddc458a2b508285c0ee10b7bd94da" +
                            "2815a0c5bd5b2e15cbe66355e42f5af8955cdfc0b3a4996d" +
                            "288db1f4b32b15643b18193e378cb7491f3c3951cdd044b1" +
                            "a519571bffac2da986f5f1d506c66530a55f70751e24fa8e" +
                            "d83ac2347f4069fb561a5565e78c6f0207da24e889a93a96" +
                            "65f717d9fe8a2938a09ab5f81be7ccecf466c0397fc15a57" +
                            "469939793f302739765773c256a3ca55d0548afd117a7cae" +
                            "98ca7e0d749a130c7b743d376848e255f8fdbe4cb4480b63" +
                            "cd2c015d1020cf095d175f3ca9dcdfbaf1b2a6e6468eee4c" +
                            "c750f2132a77f376bd9782b9d0ff4da98621b898e251a263" +
                            "4301ba2214a8c430b2f7a79dbbfd6d7ff6e9b0c137b025ff" +
                            "587c0bf912f0b19d4fff96b1ecd2ca990c89b386055c60f2" +
                            "3b94214bd55096f17a7b2c0fa12b333235101cd6f28a128c" +
                            "782e8a72671adadebbd073ded30bd7f09fb693565dcf0bf3" +
                            "090c21d13e5b0989dd8956f18f17f4f69449a13549c9d80a" +
                            "77e5e61b5aeeee9528634100e7bc390672f0ded1ca53555b" +
                            "abddbcf700b9da6192255bddf50a76b709fbed251dce4c7e" +
                            "1ca36b85d1e97c1bc9d38c887a5adf140f9eeef674c31422" +
                            "e65f63cae719f8c1324e42fa5fd8500899ef5aa3f9856aa7" +
                            "ce10c85600a040343204f36bfeab8cfa6e9deb8a2edd2a8e" +
                            "018d00c7c9fa3a251ad0f57183c37e6377797653f382ec7a" +
                            "2b0145e16d3c856bc3634b46d90d7198aff12aff88a30e34" +
                            "e2bfaf62705f3382576a9d3eeb0829fca2387b5b654af46e" +
                            "5cf6316fb57d59e5ea6c369061ac64d99671b0e516529dd5" +
                            "d9c48ea0503e55fee090d36c5ea8b5954f6fcc0060794e1c" +
                            "b7bc24aa1e5c0142fd4ce6e8fd5aa92a7bf84317ea9e1642" +
                            "b6995bac6705adf93cbce72433ed0871139970d640f67b78" +
                            "e63a7a6d849db2567df69ac7d79f8c62664ac221df228289" +
                            "d0a4f9ebd9acb4f87d49da64e51a619fd3f3baccbd9feb12" +
                            "5abe0cc2c8d17ed1d8546da2b6c641f4d3020a5f9b9f26ac" +
                            "16546c2d61385505612275ea344c2bbf1ce890023738f715" +
                            "5e9eba6a071678c8ebd009c328c3eb643679de86e69a9fa5" +
                            "67a9e146030ff03d546310a0a568c5ba0070e0da22f2cef8" +
                            "54714b04d399bbc8fd261f9e8efcd0e83bdbc3f5cfb2d024" +
                            "3e398478cc598e000124eb8858f9df8f52946c2a1ca5c400"
                    }
                }
            }
        };
    }
}
