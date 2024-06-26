#import "AdGemSdk/AdGemSdk-Swift.h"
#import "AdGemOfferwallDelegate.h"

#pragma clang diagnostic push
#pragma ide diagnostic ignored "OCUnusedGlobalDeclarationInspection"

typedef void(AdGemVoidCallbackDelegate)(void *actionPtr);
typedef void(AdGemIntCallbackDelegate)(void *actionPtr, int data);
typedef void(AdGemStringCallbackDelegate)(void *actionPtr, const char *data);

extern "C" {
NSString* createNSStringFrom(const char * cstring) {
    return [NSString stringWithUTF8String:(cstring ?: "")];
}

char* cStringCopy(const char * string) {
    char *res = (char *) malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

char* createCStringFrom(NSString* string) {
    if (!string) {
        string = @"";
    }
    return cStringCopy([string UTF8String]);
}

NSDate* parseDate(NSString* dateString) {
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
    [dateFormatter setDateFormat:@"YYYY-MM-dd HH:mm:ss"];
    return [dateFormatter dateFromString:dateString];
}

AdGemOfferwallDelegate* _adGemDelegate;

void _unregisterDelegate() {
    if (_adGemDelegate == nullptr)
        return;
    
    [AdGem setDelegate:nil];
    _adGemDelegate = nullptr;
}

void _initDelegate(AdGemVoidCallbackDelegate onLoadStarted, void* onLoadStartedPtr,
                   AdGemVoidCallbackDelegate onLoadFinished, void* onLoadFinishedPtr,
                   AdGemStringCallbackDelegate onLoadError, void* onLoadErrorPtr,
                   AdGemIntCallbackDelegate onReward, void* onRewardPtr,
                   AdGemVoidCallbackDelegate onClosed, void* onClosedPtr) {
    _unregisterDelegate();
    
    _adGemDelegate = [AdGemOfferwallDelegate new];
    
    _adGemDelegate.onOfferwallLoadingStarted = ^{
        onLoadStarted(onLoadStartedPtr);
    };
    
    _adGemDelegate.onOfferwallLoadingFinished = ^{
        onLoadFinished(onLoadFinishedPtr);
    };
    
    _adGemDelegate.onOfferwallLoadingFailedWithError = ^(NSString *error) {
        onLoadError(onLoadErrorPtr, createCStringFrom(error));
    };
    
    _adGemDelegate.onOfferwallRewardReceived = ^(int amount) {
        onReward(onRewardPtr, amount);
    };
    
    _adGemDelegate.onOfferwallClosed = ^{
        onClosed(onClosedPtr);
    };
    
    [AdGem setDelegate:_adGemDelegate];
}

void _setMetadata(const char* playerId, int age, int gender, int level, long placement, const char* createdAt, bool isPayer, float iapTotal,
                  const char* custom1, const char* custom2, const char* custom3, const char* custom4, const char* custom5) {
    auto builder = [Builder initWithPlayerIdWithPlayerId:createNSStringFrom(playerId)];
    if (age >= 0)
        builder = [builder playerAgeWithAge:age];
    if (gender >= 0)
        builder = [builder playerGenderWithGender:(gender == 0 ? AdGemGenderMale : AdGemGenderFemale)];
    if (level >= 0)
        builder = [builder playerLevelWithLevel:level];
    if (placement >= 0)
        builder = [builder playerPlacementWithPlace:placement];
    
    auto dateString = createNSStringFrom(createdAt);
    if (![dateString isEqual: @""])
        builder = [builder playerCreatedAtCreationDate:parseDate(dateString)];
    
    builder = [builder playerPayerWithSpentMoney:isPayer];
    
    if (iapTotal >= 0)
        builder = [builder playerIAPTotalWithIapTotal:iapTotal];
    
    auto custom1String = createNSStringFrom(custom1);
    if (![custom1String isEqual: @""])
        builder = [builder customField1WithField:custom1String];
    auto custom2String = createNSStringFrom(custom2);
    if (![custom2String isEqual: @""])
        builder = [builder customField2WithField:custom2String];
    auto custom3String = createNSStringFrom(custom3);
    if (![custom3String isEqual: @""])
        builder = [builder customField3WithField:custom3String];
    auto custom4String = createNSStringFrom(custom4);
    if (![custom4String isEqual: @""])
        builder = [builder customField4WithField:custom4String];
    auto custom5String = createNSStringFrom(custom5);
    if (![custom5String isEqual: @""])
        builder = [builder customField5WithField:custom5String];
    
    [AdGem setPlayerMetaDataWithMetaData:[builder build]];
}

void _showOfferwall() {
    [AdGem showOfferwall];
}
}

#pragma clang diagnostic pop
