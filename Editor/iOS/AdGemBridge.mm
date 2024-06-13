#import "AdGemSdk/AdGemSdk-Swift.h"

#pragma clang diagnostic push
#pragma ide diagnostic ignored "OCUnusedGlobalDeclarationInspection"

extern "C" {
NSString* createNSStringFrom(const char * cstring) {
    return [NSString stringWithUTF8String:(cstring ?: "")];
}

NSDate* parseDate(NSString* dateString) {
    NSDateFormatter *dateFormatter = [[NSDateFormatter alloc] init];
    [dateFormatter setDateFormat:@"YYYY-MM-dd HH:mm:ss"];
    return [dateFormatter dateFromString:dateString];
}

void _setMetadata(const char* playerId, int age, int gender, int level, int placement, const char* createdAt, bool isPayer, float iapTotal,
                  const char* custom1, const char* custom2, const char* custom3, const char* custom4, const char* custom5) {
    auto builder = [[Builder alloc] init];
    builder = [builder playerIdWithPlayerId:createNSStringFrom(playerId)];
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
}
}

#pragma clang diagnostic pop
