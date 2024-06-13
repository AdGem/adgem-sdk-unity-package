#import "AdGemSdk/AdGemSdk-Swift.h"

@interface AdGemOfferwallDelegate : NSObject <AdGemDelegate>

@property(nonatomic, copy) void (^onOfferwallLoadingStarted)();
@property(nonatomic, copy) void (^onOfferwallLoadingFinished)();
@property(nonatomic, copy) void (^onOfferwallLoadingFailedWithError)(NSString* error);
@property(nonatomic, copy) void (^onOfferwallRewardReceived)(int amount);
@property(nonatomic, copy) void (^onOfferwallClosed)();

@end
